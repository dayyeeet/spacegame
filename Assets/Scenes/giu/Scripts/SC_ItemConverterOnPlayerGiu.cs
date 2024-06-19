using UnityEngine;

public class SC_ItemConverterOnPlayerGiu : MonoBehaviour
{
    [SerializeField] private PlayerLookAndDistanceCheck playerLookAndDistance;
    [SerializeField] private FuelBar fuelBar;
    [SerializeField] private HealthBarGiu healthBar;
    
    public void GetFuel(float value)
    {
        if (fuelBar.currentValue > fuelBar.maxValue)
        {
            fuelBar.currentValue = fuelBar.maxValue;
            return;
        }
        fuelBar.currentValue += value;
    }

    public void GetHealth(float value)
    {
        if (healthBar.currentValue > healthBar.maxValue)
        {
            healthBar.currentValue = healthBar.maxValue;
            return;
        }
        healthBar.currentValue += value;
    }
}
