using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// DrowMapと密結合である
/// </summary>
[RequireComponent(typeof(Belong))]
[RequireComponent(typeof(BelongColorSetter))]
public class MapPointer : MonoBehaviour
{
    [HideInInspector] public DrowMap parentDrowMap;
    [HideInInspector] public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position * parentDrowMap.mapScaleRate + parentDrowMap.transform.position;
        //艦又は、Bulletが消失したら
        if (!target.activeInHierarchy)
        {
            parentDrowMap.mapPointerDictionary.Remove(target.gameObject);
            Destroy(gameObject);
        }
        if (target.tag == "Player")
        {
            transform.localScale = new Vector3(0.001f,0.001f,0.001f);
        }
    }
}
