using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : SliderBarController
{
    [SerializeField] Status status;
    [SerializeField] bool isLookCamera;

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
        SliderValue=status.RateRemainHp;
        if (isLookCamera)
        {
            transform.LookAt(Camera.main.transform);
        }
        
    }
}
