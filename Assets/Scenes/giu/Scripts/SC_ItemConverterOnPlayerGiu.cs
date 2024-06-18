using UnityEngine;

public class SC_ItemConverterOnPlayerGiu : MonoBehaviour
{
    [SerializeField] private PlayerLookAndDistanceCheck playerLookAndDistance;
    [SerializeField] private FuelBar fuelBar;
    [SerializeField] private HealthBarGiu healthBar;
    
    public void GetFuel(float value)
    {
        fuelBar.currentValue += value;
    }

    public void GetHealth(float value)
    {
        healthBar.currentValue += value;
    }
}
