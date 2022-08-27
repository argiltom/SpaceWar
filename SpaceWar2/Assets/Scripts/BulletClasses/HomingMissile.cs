using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : BulletBase
{
    ExplosionCaster explosionCaster;
    [SerializeField] float searchRange;
    [SerializeField] float duration;
    DelayTimer aliveTimer;
    IShip target;
    // Start is called before the first frame update
    void Start()
    {
        BulletBaseStart();
        explosionCaster = GetComponent<ExplosionCaster>();
        aliveTimer = DelayTimer.DelayTimerConstructor(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null||target.isExist==false)
        {
            target = parentShip.Controller.GetRandomEnemyShip(searchRange);
            rigidbody.MovePosition(transform.position + transform.forward * bulletSpeed * Time.deltaTime);
        }
        else
        {
            //transform.LookAt(target.transform);

            Vector3 deviation = target.transform.position - transform.position;
            var rot = Quaternion.LookRotation(deviation, transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, bulletSpeed * Time.deltaTime);
            rigidbody.MovePosition(transform.position + transform.forward * bulletSpeed * Time.deltaTime);
        }


        if (aliveTimer.IsArrival)
        {
            RemoveBullet();
        }
    }
    protected override void HitBullet()
    {
        Instantiate(bulletHitSound, transform.position, Quaternion.identity);
        explosionCaster.ExecuteExplosion();
    }
}
