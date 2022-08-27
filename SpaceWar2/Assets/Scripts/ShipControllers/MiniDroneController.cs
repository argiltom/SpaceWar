using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 小規模な偵察ドローンの挙動
/// </summary>
public class MiniDroneController : ControllerBase,IShip
{
    IShip ally;
    [SerializeField] float attackRange;
    [SerializeField] float noticeEnemyRange;
    [SerializeField] float distanceRainforcement;
    [SerializeField] ExplosionCaster explosionCaster;
    DelayTimer updateTargetTimer;
    // Start is called before the first frame update
    void Start()
    {
        ControllerBaseStart();
        updateTargetTimer = DelayTimer.DelayTimerConstructor(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        IShip tempTarget;
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

        }
        ally = GetNearestAllyShip();

        if (ally != null)
        {
            move.TakeDistanceFromDestination(ally.transform.position, distanceRainforcement, 0.1f);
        }
        if (AttackTarget != null)
        {
            if (authority.tacticalCommand == TacticalCommand.None || !authority.IsChildShip)
            {
                move.MovingToTarget(1f);
            }
            else if (authority.tacticalCommand == TacticalCommand.Follow)
            {
                FollowMove();

            }
        }

        if (move.DistanceFromTarget < move.takeDistanceFromTarget * 1.5 && AttackTarget != null)
        {
            FullFire();
        }

        if (status.IsDead)
        {
            explosionCaster.ExecuteExplosion();
            RemoveShip();
        }

    }
    protected void FollowMove()
    {
        if (AttackTarget == null)
        {
            move.LookingDestination(0.1f, authority.MyPosition);
        }
        else
        {
            move.LookingTarget(move.rotArgRate*0.1f);
            if (move.DistanceFromTarget<attackRange&&Vector3.Distance(transform.position,authority.MyPosition)<attackRange*2)
            {
                move.MovingToTarget(0.1f);
            }
            else
            {
                move.MovingToDestination(authority.MyPosition);
            }
        }
    }
}
