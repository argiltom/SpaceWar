using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 艦という単位でオブジェクトにアクセスするにはこのインタフェースからアクセスすること
/// </summary>
public interface IShip 
{
    ControllerBase Controller
    {
       get;
    }
    Status Status
    {
        get;
    }
    MoveBase Move
    {
        get;
    }
    Authority Authority
    {
        get;
    }
    Belong Belong
    {
        get;
    }
    Transform transform
    {
        get;
    }
    GameObject gameObject
    {
        get;
    }
    bool isExist
    {
        get;
    }
}
