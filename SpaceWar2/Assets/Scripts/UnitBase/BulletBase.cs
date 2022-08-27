using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Belong))]
[RequireComponent(typeof(Rigidbody))]
public abstract class BulletBase : MonoBehaviour
{
    [HideInInspector] public Belong belong;
    [HideInInspector] public IShip parentShip;
    [HideInInspector] public float damage;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float bulletSizeRate=1;
    [SerializeField] protected GameObject bulletSound;
    [SerializeField] protected GameObject bulletHitSound;
    protected Rigidbody rigidbody;
    GameManager gameManager;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        IShip targetIShip = collision.gameObject.GetComponent<IShip>();
        if (targetIShip != null&&parentShip!=null)
        {
            parentShip.Controller.AttackTo(damage, targetIShip);
            HitBullet();
            RemoveBullet();
        }
        
    }
    protected void BulletBaseStart()
    {
        belong = GetComponent<Belong>();
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameManager.GetGameManager();
        gameManager.bullets.Add(this);
        //サイズを調整
        transform.localScale = transform.localScale * bulletSizeRate;
        Instantiate(bulletSound,transform.position,Quaternion.identity);
    }
    /// <summary>
    /// 着弾時の処理、オーバーライドを要請する
    /// </summary>
    protected abstract void HitBullet();
    /// <summary>
    /// 弾丸消去時処理、自身非アクティブにし,リストから自身を消去
    /// </summary>
    protected void RemoveBullet()
    {
        if (gameManager.bullets.Contains(this))
        {
            gameManager.bullets.Remove(this);
        }
        gameObject.SetActive(false);
    }
}
