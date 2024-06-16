using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{ 
    public float maxValue = 100f;
    
    protected float CurrentValue;

    private Slider _slider;
    
    protected virtual void Start()
    {
        _slider = GetComponent<Slider>();
        CurrentValue = maxValue;
        _slider.maxValue = maxValue;
        UpdateBar();
    }

    public void SetValue(float value)
    {
        CurrentValue = value;
        UpdateBar();
    }

    protected void UpdateBar()
    {
        _slider.value = CurrentValue;
    }
}