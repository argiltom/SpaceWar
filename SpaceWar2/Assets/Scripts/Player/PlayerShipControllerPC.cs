using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// パソコン版の挙動
/// //貴方が炎を絶やさぬのなら、　我々も心の炎を燃やしましょう
/// </summary>
public class PlayerShipControllerPC : ControllerBase
{
    IShip target;
    PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        ControllerBaseStart();
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void FixedUpdate()
    {
        if (status.hp <= 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            move.MovingForward();
        }
        if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            move.MovingBack();
        }
        if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            move.MovingRight();
        }
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            move.MovingLeft();
        }
        if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.Space))
        {
            move.MovingY(move.moveSpeed);
        }
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            move.MovingY(-move.moveSpeed);
        }
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))
        {
            weapons[0].Fire();
        }
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.X))
        {
            weapons[1].Fire();
        }
        if (Input.GetMouseButton(2) || Input.GetKey(KeyCode.C))
        {
            weapons[2].Fire();
        }
        if (Input.GetKey(KeyCode.M))
        {
            JsonReadWriter.WriteData<PlayerData>(playerData, "testData");
        }
        if (Input.GetKey(KeyCode.N))
        {
            PlayerData tempData = JsonReadWriter.ReadData<PlayerData>("testData");
            status.hp = tempData.playerHp;
            status.score = tempData.playerScore;
        }
        target = GetNearestEnemyShip();
        if (target != null)
        {
            AttackTarget = target;
        }
        playerData.playerHp = status.hp;
        playerData.playerScore = status.score;
    }
}


public struct PlayerData
{
    public int playerScore;
    public float playerHp;
}
