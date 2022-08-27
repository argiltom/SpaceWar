using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BelongEnum { Players, Enemy1, Enemy2, Enemy3, Enemy4 }
/// <summary>
/// 所属を指し示す艦のコンポーネント
/// </summary>
public class Belong : MonoBehaviour
{
    /// <summary>
    /// レーダーで観測される際のサイズの割合(1が基準)
    /// </summary>
    public float rateOfsizeOnLader=1;
    /// <summary>
    /// 列挙体の実体
    /// </summary>
    [SerializeField] BelongEnum belongData;
    /// <summary>
    /// この列挙体の値によって自動的にlayerも変動する．
    /// </summary>
    public BelongEnum belongEnum
    {
        get
        {
            return belongData;
        }
        set
        {
            belongData = value;
            gameObject.layer = BelongToLayer(belongData);
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.layer = BelongToLayer(belongData);
    }
    public int BelongToLayer(BelongEnum belongEnum)
    {
        return (int)belongEnum + 8;
    }
}
