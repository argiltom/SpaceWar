using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controller 艦の挙動制御を行う部分、このクラスを継承し,艦毎固有の処理を記述する
/// //この基底クラスでは、艦の検索　、破壊時の処理 、 攻撃のメソッド,IShipの実装等の機能を提供する
/// //この基底クラスでは、行動は一切指定しない、行動は継承先で指定されるべきである
/// </summary>
[RequireComponent(typeof(Belong))]
[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Authority))]
[RequireComponent(typeof(MoveBase))]


public class ControllerBase : MonoBehaviour, IShip
{
    [HideInInspector]public Status status;
    [HideInInspector] public MoveBase move;
    [HideInInspector] public Authority authority;
    [HideInInspector] public Belong belong;
    [HideInInspector] public GameManager gameManager;
    

    public List<Weapon> weapons;

    protected List<IShip> Ships{
        get{
            return gameManager.ships;
        }
    }

    //攻撃対象管理
    private IShip attackTarget;
    /// <summary>
    /// 攻撃対象
    /// </summary>
    public IShip AttackTarget
    {
        get
        {
            return attackTarget;
        }
        set
        {
            attackTarget = value;
            try
            {
                move.target = attackTarget.transform;
            }catch(System.Exception ex)
            {
                Debug.Log("move.target=" + move.target==null);
                Debug.Log("attackTarget=" + attackTarget==null);
                //Debug.LogError(ex.ToString() + gameObject.name);
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        
    }
    /// <summary>
    /// ControllerBase初期化処理
    /// </summary>
    public void ControllerBaseStart()
    {
        status = GetComponent<Status>();
        move = GetComponent<MoveBase>();
        authority = GetComponent<Authority>();
        belong = GetComponent<Belong>();
        status.hp = status.MaxHp;
        gameManager = GameManager.GetGameManager();
        //武器の所属関係のセット
        foreach(Weapon weapon in weapons)
        {
            weapon.ship = this;
        }


        gameManager.ships.Add(this);
    }
    public void AttackTo(float damage,IShip ship)
    {
        if (ship.Status.def < damage)
        {
            ship.Status.hp -= damage - ship.Status.def;
            //ダメージを与えられたら自身にスコア
            this.status.score++;
        }
        
    }
    public IShip GetNearestEnemyShip()
    {
        float minDis=-1;
        Vector3 myPos = transform.position;
        IShip retShip=null;
        foreach(IShip ship in Ships)
        {
            //自分と引数に指定した勢力を除外
            if (ship.Belong.belongEnum == this.belong.belongEnum || ship.Equals(this) || !isExist)
            {
                continue;
            }
            Vector3 shipPos = ship.transform.position;
            
            float tempDis = Mathf.Abs(shipPos.x - myPos.x) + Mathf.Abs(shipPos.y - myPos.y) + Mathf.Abs(shipPos.z - myPos.z);
            if (minDis == -1 || minDis > tempDis)
            {
                minDis = tempDis;
                retShip = ship;
            }

        }
        return retShip;
    }
    /// <summary>
    /// 範囲内の敵艦のリストを得る
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<IShip> GetEnemyShipList(float range)
    {
        Vector3 myPos = transform.position;
        List<IShip> retShipList = new List<IShip>();
        foreach (IShip ship in Ships)
        {
            //自分と引数に指定した勢力を除外 さらに、死んでいるユニットを除外
            if (ship.Belong.belongEnum == this.belong.belongEnum || ship.Equals(this)||!isExist)
            {
                continue;
            }
            Vector3 shipPos = ship.transform.position;

            float distance =Vector3.Distance(myPos,shipPos) ;
            if (distance<=range)
            {
                retShipList.Add(ship);
            }

        }
        return retShipList;
    }

    public IShip GetRandomEnemyShip(float range)
    {
        List<IShip> ships = GetEnemyShipList(range);
        if (ships.Count == 0) return null;

        return ships[Random.Range(0, ships.Count)];
    }
    public IShip GetNearestAllyShip()
    {
        float minDis = -1;
        Vector3 myPos = transform.position;
        IShip retShip = null;
        foreach (IShip ship in Ships)
        {
            //自分と自分の勢力以外を除外
            if (ship.Belong.belongEnum != this.belong.belongEnum || ship.Equals(this)|| !isExist)
            {
                continue;
            }
            Vector3 shipPos = ship.transform.position;

            float tempDis = Mathf.Abs(shipPos.x - myPos.x) + Mathf.Abs(shipPos.y - myPos.y) + Mathf.Abs(shipPos.z - myPos.z);
            if (minDis == -1 || minDis > tempDis)
            {
                minDis = tempDis;
                retShip = ship;
            }
        }
        return retShip;
    }

    public IShip SummonShip(GameObject summonShip,Vector3 summonPos)
    {
        GameObject summonObj = Instantiate(summonShip,summonPos,Quaternion.identity);
        IShip ship = summonObj.GetComponent<IShip>();
        Belong shipBelong = summonObj.GetComponent<Belong>();
        //Debug.Log(ship);
        shipBelong.belongEnum = this.belong.belongEnum;
        return ship;
    }

    public IShip SummonShip(GameObject summonShip, Vector3 summonPos,bool isMergeToFleet)
    {
        GameObject summonObj = Instantiate(summonShip, summonPos, Quaternion.identity);
        IShip ship = summonObj.GetComponent<IShip>();
        Belong shipBelong = summonObj.GetComponent<Belong>();
        shipBelong.belongEnum = this.belong.belongEnum;

        Authority shipAuthority = summonObj.GetComponent<Authority>();

        if (isMergeToFleet)
        {
            Authority.MergeToFleet(shipAuthority);
        }
        return ship;
    }
    public void FullFire()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.Fire();
        }
    }
    /// <summary>
    /// 艦が沈んだ時の処理、自身非アクティブにし,リストから自身を消去
    /// </summary>
    protected void RemoveShip()
    {
        if (gameManager.ships.Contains(this))
        {
            gameManager.ships.Remove(this);
        }
        gameObject.SetActive(false);
    }

    protected void OnDestroy()
    {
        if (gameManager.ships.Contains(this))
        {
            gameManager.ships.Remove(this);
        }
    }

    


    //Imprements of Iship-----------------------------------------
    public ControllerBase Controller
    {
        get
        {
            return this;
        }
    }
    public Status Status
    {
        get
        {
            return status;
        }
    }

    public MoveBase Move
    {
        get
        {
            return move;
        }

    }
    public Authority Authority
    {
        get
        {
            return authority;
        }

    }
    public Belong Belong
    {
        get
        {
            return belong;
        }

    }
    public bool isExist
    {
        get
        {
            return gameObject.activeSelf;//これで無効化されているものを除外する
        }
    }
    //Componentクラスでtranceformは実装済み
    //ComponentクラスでGameObjectは実装済み

    //-----------------------------------------

}
