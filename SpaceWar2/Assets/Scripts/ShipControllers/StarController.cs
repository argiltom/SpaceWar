using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : ControllerBase
{
    [SerializeField] List<SummonShipStruct> summonShips;
    List<DelayTimer> delayTimers= new List<DelayTimer>();
    [SerializeField] ExplosionCaster explosionCaster;
    // Start is called before the first frame update
    void Start()
    {
        
        ControllerBaseStart();
        foreach(SummonShipStruct summonShipStruct in summonShips)
        {
            delayTimers.Add(DelayTimer.DelayTimerConstructor(gameObject, summonShipStruct.summonDelay));
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        for (i = 0; i < summonShips.Count; i++)
        {
            if (delayTimers[i].IsArrival)
            {
                SummonShip(summonShips[i].summonShip, transform.position+Vec3Ramdam(-10,10));
                delayTimers[i].ResetCount();
            }
        }

        if (status.IsDead)
        {
            explosionCaster.ExecuteExplosion();
            RemoveShip();
        }
    }
    [System.Serializable]
    public struct SummonShipStruct
    {
        public GameObject summonShip;
        public float summonDelay;
    }
    public Vector3 Vec3Ramdam(float min,float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
} 

