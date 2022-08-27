using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 艦隊を生成するMonoクラス FormationBaseを継承するクラスを必ず併用すること
/// </summary>
[RequireComponent(typeof(Belong))]
public class ShipFleetSetter : MonoBehaviour
{
    [SerializeField] FormationBase formation;
    [SerializeField] GameObject flagShip;
    [SerializeField] List<GameObject> childShips;
    GameObject flagShipObj;
    Authority flagShipAuthority;

    
    Belong belong;
    // Start is called before the first frame update
    //やはりインターフェース経由で、GameObjectにアタッチされたMonoBehavior継承クラスにアクセスすると、NullReferenceExceptionを投げる
    void Start()
    {
        belong = GetComponent<Belong>();
        flagShipObj = Instantiate(flagShip, transform.position, transform.rotation);
        flagShipObj.GetComponent<Belong>().belongEnum = belong.belongEnum;
        flagShipAuthority = flagShipObj.GetComponent<Authority>();
        flagShipAuthority.fleetAttackRange = formation.fleetAttackRange;
        foreach(GameObject childShip in childShips)
        {
            GameObject shipObj = Instantiate(childShip, transform.position, transform.rotation);
            //勢力適用
            shipObj.GetComponent<Belong>().belongEnum=belong.belongEnum;

            Authority authority = shipObj.GetComponent<Authority>();
            flagShipAuthority.MergeToFleet(authority);
         
        }
        //フォーメーションをセット
        formation.SetFormation(flagShipAuthority.childrenAuthoritys);
        flagShipAuthority.CommandToFleet(TacticalCommand.Follow);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
