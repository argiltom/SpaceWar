using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武装ごとに、付与する　
/// </summary>
public class Weapon : MonoBehaviour
{
    public IShip ship;
    [SerializeField] string weaponName;
    [SerializeField] GameObject bullet;

    //[SerializeField] float attackDelay;
    [SerializeField] Vector2 attackDelayRange = new Vector2(0,1);
    float maxTimeCount;
    float timeCount;
    //ダメージ設定部
    [SerializeField] float reflectAtkRate = 1;
    [SerializeField] float fixDamage = 0;
    //弾速設定部
    [SerializeField] float bulletSpeed;
    //弾丸サイズ設定部 1が基準
    [SerializeField] float bulletSizeRate = 1;
    //砲塔回転部
    Quaternion defaultRotation;
    //弾丸発射地点(nullなら砲塔から発射する)
    [SerializeField] Transform shootingPoint;
    public bool isLookingDestination = true;
    public float rotArg =0.5f;
    Transform target
    {
        get
        {
            return ship.Move.target;
        }
    }
    //DelayTimer delayTimer;
    public string WeaponName
    {
        get
        {
            return weaponName;
        }
        
    }


    protected void Start()
    {
        //delayTimer = DelayTimer.DelayTimerConstructor(gameObject, attackDelay);
        maxTimeCount = Random.Range(attackDelayRange.x, attackDelayRange.y);
        defaultRotation = transform.rotation;
    }
    protected void Update()
    {
        if (isLookingDestination)
        { 
            if (target!=null)
            {
                 LookingDestination(rotArg);
            }
            else
            {
                SettingDefaultRotation(rotArg);
            }
        }
        timeCount += Time.deltaTime;
    }
    public void Fire()
    {
        if (timeCount>=maxTimeCount)
        {
            GameObject summoned;
            if (shootingPoint == null)
            {
                summoned = Instantiate(bullet, transform.position, transform.rotation);
            }
            else
            {
                summoned = Instantiate(bullet,shootingPoint.position, transform.rotation);
            }
            
            
            BulletBase summonedBulletBase = summoned.GetComponent<BulletBase>();
            summonedBulletBase.damage = ship.Status.atk * reflectAtkRate + fixDamage;
            summonedBulletBase.parentShip = ship;
            summonedBulletBase.bulletSpeed = bulletSpeed;
            summonedBulletBase.bulletSizeRate = bulletSizeRate;
            
            Belong summonedBulletBelong = summoned.GetComponent<Belong>();
            summonedBulletBelong.belongEnum = ship.Belong.belongEnum;
            timeCount = 0;
            maxTimeCount = Random.Range(attackDelayRange.x, attackDelayRange.y);
            //delayTimer.ResetCount();
        }
        
    }
    protected void LookingDestination(float rotArg)
    {
        //目的地の方を向く
        Vector3 deviation = target.position - transform.position;
        var rot = Quaternion.LookRotation(deviation, transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotArg);
    }
    protected void SettingDefaultRotation(float rotArg)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, rotArg);
    }
}
