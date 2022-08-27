using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 突撃陣形 XとYとZをそれぞれ別々に関数として定義する
/// </summary>
public class NeedleFormation : FormationBase
{
    /// <summary>
    /// 艦同士の距離
    /// </summary>
    [SerializeField] float distance;
    [SerializeField] float longOfForward;
   
    protected override Vector3 GetOffsetPosition(int index)
    {
        int formIndex = index;
        Vector3 retVec = Vector3.zero;
        //int maxZ = GetZ(sizeOfList);
        int tempZ = GetZ(formIndex);
        retVec.x = distance * (GetX(formIndex)-tempZ/2.0f);
        retVec.y = distance * (GetY(formIndex)-tempZ/2.0f);
        retVec.z = -distance * tempZ+longOfForward;

        return retVec;
    }
    private int GetZ(int n)
    {
        float temp1 = n+1;
        int z = 1;
        while (temp1 - Mathf.Pow(z, 2) > 0)
        {
            temp1 = temp1 - Mathf.Pow(z, 2);
            z++;
        }
        return z;
    }
    private int GetX(int n)
    {
        float temp1 = n + 1;
        int z = 1;
        while (temp1 - Mathf.Pow(z, 2) > 0)
        {
            temp1 = temp1 - Mathf.Pow(z, 2);
            z++;
        }
        return Mathf.FloorToInt(temp1)%z;
    }
    private int GetY(int n)
    {
        float temp1 = n + 1;
        int z = 1;
        while (temp1 - Mathf.Pow(z, 2) > 0)
        {
            temp1 = temp1 - Mathf.Pow(z, 2);
            z++;
        }
        return Mathf.CeilToInt(temp1 / z);
    }
}
