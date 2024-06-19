using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_CurrentPlanetText : MonoBehaviour
{
    [SerializeField] private string currentPlanetName;
    
    private TextMeshProUGUI _currentPlanetText;
    
    private void Start()
    {
        _currentPlanetText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        _currentPlanetText.text = $"{currentPlanetName}";
    }
}
