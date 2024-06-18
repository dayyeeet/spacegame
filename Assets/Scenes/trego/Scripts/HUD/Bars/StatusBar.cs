using System;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public float maxValue = 100f;
    
    public float currentValue;

    protected Slider Slider;
    
    protected virtual void Start()
    {
        Slider = GetComponent<Slider>();
        Slider.maxValue = maxValue;
        UpdateBar();
    }

    protected virtual void SetValue(float value)
    {
        currentValue = value;
        UpdateBar();
    }

    protected void UpdateBar()
    {
        Slider.value = currentValue;
    }
}