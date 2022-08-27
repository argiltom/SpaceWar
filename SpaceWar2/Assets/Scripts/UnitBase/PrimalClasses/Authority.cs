using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 艦隊指揮・披指揮 艦同士の関係が木構造で構成されるようになる
/// </summary>
public class Authority : MonoBehaviour
{
    public TacticalCommand tacticalCommand;
    public Authority parentAuthority;
    public List<Authority> childrenAuthoritys = new List<Authority>();
    public Vector3 myOffsetPosition;
    DelayTimer flushingTimer;
    //-------------------艦隊全体制御-----------------------
    /// <summary>
    /// 旗艦を中心とする艦隊全体の攻撃範囲 
    /// </summary>
    public float fleetAttackRange = 0;

    
    //艦隊からの攻撃支援要請があるならtrue
    public bool isFleetFire;
    


    //-------------------------------------------------------
    public Vector3 MyPosition
    {
        get
        {
            if (IsChildShip) {
                //Offsetは親の向いている向きによって回転する 但し,z軸回転のみ無視する(余りにもカオスすぎるので)
                Vector3 tempRotVec = parentAuthority.transform.rotation.eulerAngles;
                tempRotVec = new Vector3(tempRotVec.x, tempRotVec.y, 0);
                Quaternion rot = Quaternion.Euler(tempRotVec);

                return parentAuthority.transform.position + rot*myOffsetPosition;
                //return parentAuthority.transform.position + myOffsetPosition;
            }
            else
            {
                Debug.LogWarning("親がいないので、zeroVectorを返します");
                return Vector3.zero;
            }
            
        }
    }

    
    /// <summary>
    /// 自分が一隻以上の子艦を指揮する、「親の艦」であるならture
    /// </summary>
    public bool IsParentShip
    {
        get
        {
            return childrenAuthoritys.Count >= 1;
        }
    }
    /// <summary>
    /// 自分に親がいる艦ならture　　(親のAuthorityが存在し且つ、それがアクティブなオブジェクトならtrueを返す)
    /// </summary>
    public bool IsChildShip
    {
        get
        {
            
            if (parentAuthority == null)
            {
                return false;
            }
            return parentAuthority.gameObject.GetComponent<IShip>().isExist;
        }
    }

    public float DistanceFromMyPosition
    {
        get
        {
            if (IsChildShip)
            {
                //Debug.Log(gameObject.name + ":親います");
                return Vector3.Distance(transform.position, MyPosition);
            }
            else
            {
                //Debug.Log(gameObject.name+":親の艦がいません");
                return 0;
            }
            
        }
    }
    /// <summary>
    /// 自分の麾下の艦が、所定の位置から平均してどれだけ離れているかの距離
    /// </summary>
    public float AverageDistanceOfChildShips
    {
        get
        {
            if (IsParentShip)
            {
                float retSum=0;
                int amount = childrenAuthoritys.Count;
                foreach(Authority authority in childrenAuthoritys)
                {
                    retSum += authority.DistanceFromMyPosition;
                }
                return retSum / amount;
            }
            else
            {
                Debug.Log(gameObject.name + ":子の艦がいません");
                return 0;
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        flushingTimer = DelayTimer.DelayTimerConstructor(gameObject,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flushingTimer.IsArrival)
        {
            Flushing();
            flushingTimer.ResetCount();
        }
        
        //Debug.Log(gameObject.name + "isParent=" + IsParentShip.ToString() + " isChild=" + IsChildShip.ToString());
    }

    private void Flushing(){
        //flagshipのみがこれを実行する．
        if (!IsParentShip) {
            return;
        }

        foreach (Authority authority in childrenAuthoritys)
        {
            
            if (!authority.gameObject.activeInHierarchy)
            {
                childrenAuthoritys.Remove(authority);
            }
            else
            {
                authority.fleetAttackRange = fleetAttackRange;
                authority.isFleetFire = isFleetFire;
            }
            
        }
    }
    /// <summary>
    /// 自分の子艦に指示を出す．
    /// </summary>
    /// <param name="command"></param>
    public void CommandToFleet(TacticalCommand command)
    {
        //ParentShipのみがこれを実行する．
        if (!IsParentShip)
        {
            return;
        }
        foreach (Authority authority in childrenAuthoritys)
        {
            authority.tacticalCommand = command;
        }
    }
    /// <summary>
    /// 引数に取った艦を自身の子として併合する．
    /// </summary>
    /// <param name="authority"></param>
    public void MergeToFleet(Authority authority)
    {
        //Debug.Log(this.transform);
        //その艦を自分の子供に追加
        childrenAuthoritys.Add(authority);
        //その艦の親を自分にする
        authority.parentAuthority = this;
        //Debug.Log(null==authority.parentAuthority);
    } 

    public void SetFleetFormationRamdom(float radius)
    {
        foreach (Authority authority in childrenAuthoritys)
        {
            authority.myOffsetPosition = new Vector3(Random.Range(0,radius), Random.Range(0, radius), Random.Range(0, radius));
        }
        
    }
    
}
/// <summary>
/// Authority内で用いられるの
/// </summary>
public enum TacticalCommand {None,Follow,Charge}

