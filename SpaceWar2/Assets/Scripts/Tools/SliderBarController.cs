using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderBarController : MonoBehaviour
{
    [SerializeField]  Slider slider;
    [SerializeField] Image fill;
    [SerializeField] Image backGround;
    [SerializeField] Color fillColor;
    [SerializeField] Color backGroundColor;
    public float SliderValue
    {
        set
        {
            if (value > 1)
            {
                slider.value = 1;
            }
            else if (value<0)
            {
                slider.value = 0;
            }
            else
            {
                slider.value = value;
            }
        }
        get
        {
            return slider.value;
        }
    }
    protected virtual void Start()
    {
        fill.color = fillColor;
        backGround.color = backGroundColor;
    }
}
