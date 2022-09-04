using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShip : ControllerBase
{
    IShip tempTarget;
    IShip ally;
    DelayTimer updateTargetTimer;
    [SerializeField] ExplosionCaster explosionCaster;
    [SerializeField] float takeDistanceFromRainForcement;
    [SerializeField] float attackRange;
    [SerializeField] GameObject onDestorySound;


    // Start is called before the first frame update
    void Start()
    {
        ControllerBaseStart();
        updateTargetTimer = DelayTimer.DelayTimerConstructor(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
       
        ally = GetNearestAllyShip();

        if (ally != null)
        {
            move.TakeDistanceFromDestination(ally.transform.position, takeDistanceFromRainForcement, 0.1f);
        }
        if (authority.tacticalCommand == TacticalCommand.None||!authority.IsChildShip)
        {
            if (authority.IsParentShip)
            {
                    ParentShipMove();
            }
            else
            {
                    IsolationMove();
            }

        }
        else if (authority.tacticalCommand == TacticalCommand.Follow&&authority.IsChildShip)
        {
             FollowMove();
        }
        

        
        
        if (status.IsDead)
        {
            Instantiate(onDestorySound, transform.position, Quaternion.identity);
            explosionCaster.ExecuteExplosion();
            RemoveShip();
        }
    }

    protected void IsolationMove()
    {
        if (updateTargetTimer.IsArrival)
        {

            tempTarget = GetRandomEnemyShip(attackRange);
            if (tempTarget == null)
            {
                AttackTarget = GetNearestEnemyShip();
            }
            else
            {
                AttackTarget = tempTarget;
            }
            updateTargetTimer.ResetCount();
        }

        if (AttackTarget != null)
        {
            move.MovingToTarget(1f);
        }
        if (move.DistanceFromTarget < attackRange && AttackTarget != null)
        {
            FullFire();
        }
    }


    protected void FollowMove()
    {
        if (updateTargetTimer.IsArrival)
        {
            tempTarget = GetRandomEnemyShip(authority.fleetAttackRange);
            if (tempTarget == null)
            {
                AttackTarget = GetNearestEnemyShip();
            }
            else
            {
                AttackTarget = tempTarget;
            }
            updateTargetTimer.ResetCount();
        }

        if (authority.isFleetFire && AttackTarget != null)
        {
            FullFire();
        }

        if (AttackTarget == null)
        {
            move.LookingDestination(0.1f, authority.MyPosition);
        }
        else
        {
            move.LookingTarget(move.rotArgRate);

            move.MovingToDestination(authority.MyPosition,2);

        }
    }

    protected void ParentShipMove()
    {
        if (updateTargetTimer.IsArrival)
        {

            tempTarget = GetRandomEnemyShip(authority.fleetAttackRange);
            if (tempTarget == null)
            {
                AttackTarget = GetNearestEnemyShip();
            }
            else
            {
                AttackTarget = tempTarget;
            }
            updateTargetTimer.ResetCount();
        }



        if ( authority.AverageDistanceOfChildShips > 4)
        {
            //Debug.Log("val:" + authority.AverageDistanceOfChildShips);
            //麾下の艦が所定の位置に就くまで待機
        }
        else if (authority.isFleetFire)
        {
            //攻撃中なので待機
            FullFire();
        }
        else
        {
            if (AttackTarget != null)
            {
                move.MovingToTarget(1f);
            }
            
        }
        //艦隊攻撃範囲内に敵がいるのなら
        List<IShip> serachedShips = GetEnemyShipList(authority.fleetAttackRange);
        if (serachedShips.Count >= 1)
        {
            authority.isFleetFire = true;
        }
        else
        {
            authority.isFleetFire = false;
        }
    }
}
