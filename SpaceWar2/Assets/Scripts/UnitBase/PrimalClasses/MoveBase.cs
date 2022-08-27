using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Move 移動部 PleaseCall in FixedUpdate
/// </summary>
[RequireComponent(typeof(Rigidbody))]  
public class MoveBase : MonoBehaviour
{
    Rigidbody rigidbody;
    public float moveSpeed;
    public Vector3 rotArgRate= new Vector3(1,1,1);
    /// <summary>
    /// 撃破対象、ターゲット
    /// </summary>
   [System.NonSerialized] public Transform target;



    public float DistanceFromTarget
    {
        get
        {
            if (target != null) return Vector3.Distance(target.position, transform.position);
            else return 0;
        }
    }

    public Vector2 takeDistanceFromTargetRange;
    [HideInInspector] public float takeDistanceFromTarget;


    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;//当たり判定と動きとしてrigidbody使いたいので
        takeDistanceFromTarget = Random.Range(takeDistanceFromTargetRange.x,takeDistanceFromTargetRange.y);
    }
    public void MovingForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //rigidbody.position += transform.forward * moveSpeed * Time.deltaTime;

    }

    public void MovingBack()
    {
        //rigidbody.MovePosition(transform.position - transform.forward * moveSpeed * Time.deltaTime);
        //rigidbody.position -= transform.forward * moveSpeed * Time.deltaTime;
        transform.position -= transform.forward * moveSpeed * Time.deltaTime;
    }

    public void MovingRight()
    {
        //rigidbody.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        //rigidbody.position += transform.right * moveSpeed * Time.deltaTime;
        transform.position+= transform.right * moveSpeed * Time.deltaTime;
    }

    public void MovingLeft()
    {
        //rigidbody.MovePosition(transform.position - transform.right * moveSpeed * Time.deltaTime);
        //rigidbody.position -= transform.right * moveSpeed * Time.deltaTime;
        transform.position -= transform.right * moveSpeed * Time.deltaTime;
    }

    public void MovingToTarget(float rotSpeedRate)
    {
        if (DistanceFromTarget > takeDistanceFromTarget)
        {
            MovingForward();
        }
        LookingTarget(rotArgRate);
    }



    public void MovingToDestination(Vector3 destination)
    {
        Vector3 diff = destination - transform.position;
        diff = diff.normalized;

        //rigidbody.MovePosition(transform.position + diff * moveSpeed * Time.deltaTime);
        //rigidbody.position += diff * moveSpeed * Time.deltaTime;
        transform.position += diff * moveSpeed * Time.deltaTime;
    }
    public void MovingToDestination(Vector3 destination,float takeDistance)
    {
        Vector3 diff = destination - transform.position;
        diff = diff.normalized;

        //rigidbody.MovePosition(transform.position + diff * moveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(destination, transform.position);
        if (distance > takeDistance)
        {
            transform.position += diff * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDistanceFromDestination(Vector3 destination, float takeDistance,float speedRate)
    {
        Vector3 diff = transform.position - destination ;
        diff = diff.normalized;

        //rigidbody.MovePosition(transform.position + diff * moveSpeed * Time.deltaTime);
        float distance = Vector3.Distance(destination, transform.position);
        if (distance <= takeDistance)
        {
            rigidbody.position += diff * moveSpeed*speedRate * Time.deltaTime;
        }
    }



    //引数指定型
    public void MovingForward(float speed)
    {
        //rigidbody.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        rigidbody.position += transform.forward * speed * Time.deltaTime;
    }
    public void MovingToDestination(float speed,Vector3 destination)
    {
        Vector3 diff = destination - transform.position;
        diff = diff.normalized;

        //rigidbody.MovePosition(transform.position + diff * speed * Time.deltaTime);
        rigidbody.position += diff * speed * Time.deltaTime;
    }
    public void MovingX(float speed)
    {
        //rigidbody.MovePosition(transform.position + new Vector3(1,0,0) * speed * Time.deltaTime);
        //rigidbody.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
    }

    public void MovingY(float speed)
    {
        //rigidbody.MovePosition(transform.position + new Vector3(0,1, 0) * speed * Time.deltaTime);
        //rigidbody.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
    }
    public void MovingZ(float speed)
    {
        //rigidbody.MovePosition(transform.position + new Vector3(0, 0, 1) * speed * Time.deltaTime);
        //rigidbody.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
    }
    public void LookingTarget(Vector3 rotArgRate)
    {
        //ターゲット
        Vector3 deviation = target.position - transform.position;
        var rot = Quaternion.LookRotation(deviation, transform.position);
        float slerpX = Quaternion.Slerp(transform.rotation, rot, rotArgRate.x).eulerAngles.x;
        float slerpY = Quaternion.Slerp(transform.rotation, rot, rotArgRate.y).eulerAngles.y;
        float slerpZ = Quaternion.Slerp(transform.rotation, rot, rotArgRate.z).eulerAngles.z;
        transform.rotation = Quaternion.Euler(slerpX, slerpY, slerpZ);
    }

    public void LookingTarget(float rotArg,AxisEnum freezeAxis)
    {
        Vector3 deviation = target.position - transform.position;
        var rot = Quaternion.LookRotation(deviation, transform.position);
        Quaternion tempRot = Quaternion.Slerp(transform.rotation, rot, rotArg);
        Vector3 factor = new Vector3(1, 1, 1);
        if (freezeAxis == AxisEnum.X)
        {
            factor.x = 0;
        }else if (freezeAxis == AxisEnum.Y)
        {
            factor.y = 0;
        }
        else if (freezeAxis == AxisEnum.Z)
        {
            factor.z = 0;
        }
        Vector3 eulerAng = tempRot.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(eulerAng.x*factor.x,eulerAng.y*factor.y,eulerAng.z*factor.z);
    }



    public void LookingDestination(float rotArg,Vector3 destination)
    {
        
        //目的地の方を向く
        Vector3 deviation = destination - transform.position;
        if (deviation.Equals(Vector3.zero)) return;
        var rot = Quaternion.LookRotation(deviation, transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotArg);
    }

    
}
public enum AxisEnum {X,Y,Z};