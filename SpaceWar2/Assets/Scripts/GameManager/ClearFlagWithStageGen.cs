using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearFlagWithStageGen : MonoBehaviour
{ 
    [SerializeField] StageGenerator stageGenerator;
    [SerializeField] UnityEvent clearEvent;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        stageGenerator.StageGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager == null) gameManager = GameManager.GetGameManager();

        if (stageGenerator.isStageGenerated)
        {
            if (gameManager.NumberOfShips(BelongEnum.Enemy1) == 0
            && gameManager.NumberOfShips(BelongEnum.Enemy2) == 0
            && gameManager.NumberOfShips(BelongEnum.Enemy3) == 0
            && gameManager.NumberOfShips(BelongEnum.Enemy4) == 0)
            {
                clearEvent.Invoke();
            };
        }
    }
}
