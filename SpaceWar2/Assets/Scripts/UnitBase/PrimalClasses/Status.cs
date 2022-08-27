using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>Status データ部 艦の個体としての情報を格納している</para>
/// <para>フィールドとプロパティのみ宣言可とする</para>
/// </summary>
public class Status : MonoBehaviour
{
    public float MaxHp;
    [HideInInspector] public float hp;
    public float RateRemainHp {
        get
        {
            return hp / MaxHp;
        }
    }
    public bool IsDead
    {
        get
        {
            if (hp <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public float atk;
    public float def;
    public int score;
    

}
