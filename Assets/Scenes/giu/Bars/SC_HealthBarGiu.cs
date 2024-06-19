using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarGiu : StatusBar
{
    public SC_RigidbodyPlayerMovement giuScript;
    protected override void Start()
    {
        Slider = GetComponent<Slider>(); 
        
        // Set it manually for a limited time
        Slider.maxValue = giuScript.maxHp;
        UpdateBar();
    }

    private void FixedUpdate()
    {
        SetValue(giuScript.returnHp());
    }
}
