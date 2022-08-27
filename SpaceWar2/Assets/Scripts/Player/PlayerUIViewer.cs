using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIViewer : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Text shipNumberText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        shipNumberText.text = "白軍:" + gameManager.NumberOfShips(BelongEnum.Players) + "\n"
        + "赤軍:" + gameManager.NumberOfShips(BelongEnum.Enemy1) + "\n";
        //+"デジクリ芝浦支部:" + gameManager.NumberOfShips(BelongEnum.Enemy2) + "\n"
        //+"デジクリワシントン支部:" + gameManager.NumberOfShips(BelongEnum.Enemy3) + "\n"
        //+"デジクリ豊洲支部:" + gameManager.NumberOfShips(BelongEnum.Enemy4) + "\n";
    }
}
