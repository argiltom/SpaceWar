using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFormation : FormationBase
{
    [SerializeField] float distance;
    protected override Vector3 GetOffsetPosition(int index)
    {
        int formIndex = index + 2;
        Vector3 retVec = Vector3.zero;
        retVec.x = distance * Mathf.Floor(formIndex / 2);
        if (formIndex % 2 == 0)
        {
            retVec.x = -retVec.x;
        }
        return retVec;
    }
}
