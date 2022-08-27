using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //インターフェースはインスペクターからアタッチ出来ない
    public List<IShip> ships = new List<IShip>();
    public List<BulletBase> bullets = new List<BulletBase>();
    // Start is called before the first frame update
    public static GameManager GetGameManager()
    {
        return GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    /// <summary>
    /// 特定の勢力の数
    /// </summary>
    /// <param name="belongEnum"></param>
    /// <returns></returns>
    public int NumberOfShips(BelongEnum belongEnum)
    {
        int ret = 0;
        foreach(IShip ship in ships)
        {
            if (belongEnum == ship.Belong.belongEnum)
            {
                ret++;
            }
        }
        return ret;
    }
}
