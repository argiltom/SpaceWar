using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// フォーメーション司る基底クラス
/// </summary>
public abstract class FormationBase :MonoBehaviour
{
    /// <summary>
    /// 引数にとったリストの全体数
    /// </summary>
    protected int sizeOfList;

    public bool isPuttingImmediate;

    public float fleetAttackRange;
    public void SetFormation(List<Authority> authorities)
    {
        sizeOfList = authorities.Count;
        foreach(Authority authority in authorities)
        {
            authority.myOffsetPosition = GetOffsetPosition(authorities.IndexOf(authority));
            if (isPuttingImmediate)
            {
                authority.gameObject.transform.position = authority.MyPosition;
            }
            
        }
        
    }

    protected abstract Vector3 GetOffsetPosition(int index);
}
