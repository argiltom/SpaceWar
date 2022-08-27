using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アタッチしたオブジェクトの衝突情報をListで格納するクラス
/// </summary>
public class CollisionStacker : MonoBehaviour
{
    public List<Collision> collisions = new List<Collision>();
    /// <summary>
    /// 衝突情報を1つ以上保持しているのなら真
    /// </summary>
    public bool isCollison
    {
        get
        {
            return collisions.Count >= 1;
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
    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision);
    }
    /// <summary>
    /// 末尾のcollisonを返し、取り出した要素をcollisionsから消す
    /// </summary>
    /// <returns></returns>
    public Collision PopCollison()
    {
        Collision ret = collisions[collisions.Count-1];
        collisions.Remove(ret);
        return ret;
    }
}
