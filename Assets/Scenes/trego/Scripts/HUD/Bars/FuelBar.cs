using System;
using UnityEngine;

public class FuelBar : StatusBar
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
