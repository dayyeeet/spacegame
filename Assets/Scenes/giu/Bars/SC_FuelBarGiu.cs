using System;
using UnityEngine;

public class FuelBarGiu : StatusBar
{
    [SerializeField] private float fuelDecreaseRate;
    private void Update()
    {
        currentValue -= fuelDecreaseRate * Time.deltaTime;
    }
    
    private void FixedUpdate()
    {
        SetValue(currentValue);
    }
}
