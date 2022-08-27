using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectFormation : FormationBase
{
    [SerializeField] float distance;
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;
    [SerializeField] Vector3 offsetFromParent;
    protected override Vector3 GetOffsetPosition(int index)
    {
        int formIndex = index;
        Vector3 retVec = Vector3.zero;
        retVec.x = distance * (GetX(formIndex)-sizeX/2);
        retVec.y = distance * (GetY(formIndex) - sizeY/ 2);
        retVec.z = distance * GetZ(formIndex);
        retVec += offsetFromParent;
        return retVec;
    }

    private int GetX(int index)
    {
        return index % sizeX;
    }
    private int GetY(int index)
    {
        int temp = Mathf.CeilToInt(index / sizeX);
        return temp%sizeY;

    }
    private int GetZ(int index)
    {
        return Mathf.CeilToInt(index / (sizeY*sizeX));

    }
}
