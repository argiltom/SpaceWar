using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Belong))]
public class NomalBullet : BulletBase
{
    [SerializeField] float duration=8;
    [SerializeField] ExplosionCaster explosionCaster;
    DelayTimer aliveTimer;

    // Start is called before the first frame update
    void Start()
    {
        BulletBaseStart();
        aliveTimer = DelayTimer.DelayTimerConstructor(gameObject,duration);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(transform.position + transform.forward * bulletSpeed * Time.deltaTime);
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
