using UnityEngine;

public class ItemConverterOnPlayer : MonoBehaviour
{
    [SerializeField] private PlayerLookAndDistanceCheck playerLookAndDistance;
    [SerializeField] private FuelBar fuelBar;
    [SerializeField] private HealthBar healthBar;
    
    public void GetFuel(float value)
    {
        fuelBar.currentValue += value;
    }

    public void GetHealth(float value)
    {
        healthBar.currentValue += value;
    }
}
