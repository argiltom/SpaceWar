using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolShipController : ControllerBase
{
    IShip ally;
    IShip tempTarget;
    [SerializeField] GameObject drone;
    DelayTimer delayTimer;
    DelayTimer updateTargetTimer;
    [SerializeField] ExplosionCaster explosionCaster;
    // Start is called before the first frame update
    void Start()
    {
        ControllerBaseStart();
        delayTimer = DelayTimer.DelayTimerConstructor(gameObject, 5);
        updateTargetTimer = DelayTimer.DelayTimerConstructor(gameObject, 1);

        //Debug.Log(Authority.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        //authority.CommandToFleet(TacticalCommand.Follow);


        if (updateTargetTimer.IsArrival)
        {

            tempTarget = GetRandomEnemyShip(move.takeDistanceFromTarget * 1.8f);
            if (tempTarget == null)
            {
                AttackTarget = GetNearestEnemyShip();
            }
            else
            {
                AttackTarget = tempTarget;
            }
            updateTargetTimer.ResetCount();
            //自分の麾下の艦隊に指示を出す
            authority.CommandToFleet(TacticalCommand.Follow);
        }
        ally = GetNearestAllyShip();

        if (ally != null)
        {
            move.TakeDistanceFromDestination(ally.transform.position, 10, 0.1f);
        }
        if (AttackTarget != null)
        {
            if (authority.tacticalCommand == TacticalCommand.None || authority.IsParentShip)
            {
                move.MovingToTarget(1f);
            }
            if (authority.tacticalCommand == TacticalCommand.Follow)
            {
                move.MovingToDestination(authority.MyPosition);

            }
        }
        if (delayTimer.IsArrival)
        {
            SummonShip(drone, transform.position, true);
            authority.SetFleetFormationRamdom(20);
            delayTimer.ResetCount();
        }
        if (status.IsDead)
        {
            explosionCaster.ExecuteExplosion();
            RemoveShip();
        }
    }
}
