using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MapPointerと密結合である
/// </summary>
public class DrowMap : MonoBehaviour
{
    public float pointerSizeRate;
    public float mapScaleRate;
    public float updateMapTime;
    [SerializeField] GameObject mapPointerShip;
    [SerializeField] GameObject mapPointerBullet;
    GameManager gameManager;
    DelayTimer delayTimer;
    public Dictionary<GameObject, MapPointer> mapPointerDictionary= new Dictionary<GameObject,MapPointer>();
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
        delayTimer = DelayTimer.DelayTimerConstructor(gameObject, updateMapTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer.IsArrival)
        {
            foreach (IShip ship in gameManager.ships)
            {

                if (!mapPointerDictionary.ContainsKey(ship.gameObject))
                {
                    GameObject pointer = Instantiate(mapPointerShip, ship.transform.position * mapScaleRate + transform.position, Quaternion.identity);
                    pointer.transform.localScale = pointer.transform.localScale * pointerSizeRate;
                    MapPointer mapPointer = pointer.GetComponent<MapPointer>();
                    mapPointerDictionary.Add(ship.gameObject, mapPointer);
                    mapPointer.parentDrowMap = this;
                    mapPointer.target = ship.gameObject;
                    Belong belong = pointer.GetComponent<Belong>();
                    belong.belongEnum = ship.Belong.belongEnum;
                    belong.rateOfsizeOnLader = ship.Belong.rateOfsizeOnLader;
                    pointer.transform.localScale = pointer.transform.localScale * belong.rateOfsizeOnLader;
                    //pointer.transform.rotation = ship.transform.rotation;
                }
                
            }
            
            foreach (BulletBase bullet in gameManager.bullets)
            {
                if (!mapPointerDictionary.ContainsKey(bullet.gameObject))
                {
                    GameObject pointer = Instantiate(mapPointerBullet, bullet.transform.position * mapScaleRate + transform.position, Quaternion.identity);
                    pointer.transform.localScale = pointer.transform.localScale * pointerSizeRate;
                    MapPointer mapPointer = pointer.GetComponent<MapPointer>();
                    mapPointerDictionary.Add(bullet.gameObject, mapPointer);
                    mapPointer.parentDrowMap = this;
                    mapPointer.target = bullet.gameObject;
                    Belong belong = pointer.GetComponent<Belong>();
                    belong.belongEnum = bullet.belong.belongEnum;
                    belong.rateOfsizeOnLader = bullet.belong.rateOfsizeOnLader;
                    pointer.transform.localScale = pointer.transform.localScale * belong.rateOfsizeOnLader;
                    pointer.transform.rotation = bullet.transform.rotation;
                }
                
            }
            
            delayTimer.ResetCount();
        }
        
    }
}
