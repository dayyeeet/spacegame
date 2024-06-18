using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : StatusBar
{
    [SerializeField] private SC_RigidbodyPlayerMovement giuScript;
    protected override void Start()
    {
        Slider = GetComponent<Slider>(); 
        
        // Set it manually for a limited time
        Slider.maxValue = 5;
        UpdateBar();
    }

    private void FixedUpdate()
    {
        SetValue(giuScript.sprintTime);
    }

}
