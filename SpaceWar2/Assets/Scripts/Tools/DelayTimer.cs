using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アクションの周期的Delayを制御するためのクラス
/// </summary>
public class DelayTimer :MonoBehaviour
{
    public float maxDelayTime;
    float delayTimeCounter;
    public float acceleration=1;
    public float rateOfcharge
    {
        get
        {
            return delayTimeCounter / maxDelayTime;
        }
    }
    /// <summary>
    /// <para>世界を構成するもの、或いはコンストラクタ </para>
    /// <para>MonoBehaviourを継承セシものは、newによって生成されるべからず</para>
    /// <para>MonoBehaviourを継承セシものは、AddComponentによって生成されるべし</para>
    /// </summary>
    static public DelayTimer DelayTimerConstructor(GameObject gameObject,float maxDelayTime)
    {
        DelayTimer retDelayTimer;
        retDelayTimer =gameObject.AddComponent<DelayTimer>();
        retDelayTimer.maxDelayTime = maxDelayTime;
        retDelayTimer.TimeStart();
        return retDelayTimer;
    }
    //TimeStart
    public void TimeStart()
    {
        StartCoroutine(Ticktock());
    }

    /// <summary>
    /// 開闢せし時来りて,真を返す
    /// </summary>
    /// <returns></returns>
    public bool IsArrival
    {
        get
        {
            if (delayTimeCounter >= maxDelayTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
    /// <summary>
    /// さあ、時を戻そう
    /// </summary>
    public void ResetCount()
    {
        delayTimeCounter = 0;
    }
    /// <summary>
    /// 時を刻め, 因果は加速し,蓋然は必然となる
    /// </summary>
    IEnumerator Ticktock()
    {
        while (true)
        {
            if (delayTimeCounter <= maxDelayTime)
            {
                delayTimeCounter += Time.deltaTime * acceleration;
                
            }
            yield return null;
        }
    }
}
