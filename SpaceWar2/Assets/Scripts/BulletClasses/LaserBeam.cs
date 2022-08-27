using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : BulletBase
{
    [SerializeField] float duration = 8;
    [SerializeField] ExplosionCaster explosionCaster;
    [SerializeField] GameObject laserCylinder;
    /// <summary>
    /// laserCylinderの衝突情報を格納している．
    /// </summary>
    [SerializeField] CollisionStacker collisionStacker;

    /// <summary>
    /// レーザーが貫通効果を持つか?
    /// </summary>
    [SerializeField] bool isPenetrate;
    bool isHit=false;

    DelayTimer aliveTimer;
    Vector3 baseLaserCylinderSize;
    Vector3 baseLaserCylinderLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        BulletBaseStart();
        //自身のレイヤーをレーザーへコピー
        laserCylinder.GetComponent<Belong>().belongEnum = belong.belongEnum;
        //レーザーのlocalSize.zを1に戻す
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z / bulletSizeRate); 
        
        aliveTimer = DelayTimer.DelayTimerConstructor(gameObject, duration);
        baseLaserCylinderSize = laserCylinder.transform.localScale;
        baseLaserCylinderLocalPosition = laserCylinder.transform.localPosition;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(transform.position + transform.forward * bulletSpeed * Time.deltaTime);
        if (aliveTimer.IsArrival)
        {
            RemoveBullet();
        }
        float gaussRate = 1-Mathf.Abs(aliveTimer.rateOfcharge - 0.5f)*2;
        //レーザー太さ調節 yは据え置き
        laserCylinder.transform.localScale =new Vector3(baseLaserCylinderSize.x * gaussRate,laserCylinder.transform.localScale.y,baseLaserCylinderSize.z*gaussRate);

        if (!isHit || isPenetrate) isHit = CollisonExecute();


        //貫通状態
        if (!isHit || isPenetrate)
        {
            //レーザーが伸びていく処理 xとzは据え置き
            laserCylinder.transform.localScale = new Vector3(laserCylinder.transform.localScale.x, laserCylinder.transform.localScale.y+bulletSpeed*Time.deltaTime, laserCylinder.transform.localScale.z);
            //レーザーの位置調節 xとzは据え置き
            laserCylinder.transform.localPosition = new Vector3(laserCylinder.transform.localPosition.x, laserCylinder.transform.localPosition.y, baseLaserCylinderLocalPosition.z + laserCylinder.transform.localScale.y);
        
            
        }
        
    }
    protected override void HitBullet()
    {
        //使わない
        
    }
    /// <summary>
    /// 格納している衝突情報を処理していく 衝突が起きたならtrueを返す．
    /// </summary>
    private bool CollisonExecute()
    {
        bool resultBool = false;
        while (collisionStacker.isCollison)
        {
            Collision collision = collisionStacker.PopCollison();
            //衝突処理
            IShip targetIShip = collision.gameObject.GetComponent<IShip>();
            if (targetIShip != null && parentShip != null)
            {
                parentShip.Controller.AttackTo(damage, targetIShip);
                Instantiate(bulletHitSound, targetIShip.transform.position,Quaternion.identity);
                explosionCaster.ExecuteExplosion(targetIShip.transform.position);
                resultBool = true;
                //RemoveBullet();
            }
        }
        return resultBool;
    }

    
}
