using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI[] introTexts;

    private Image _startPanelImage;
    private bool _isFadeTime;
    
    private void Start()
    {
        InitializePhase();
        
        Invoke(nameof(FadeDelay), 4f);
        
        // Play motor sound
        
        // Invoke spaceship movement with delay
        
        // Show texts with invoke after spaceship enter the camera
    }

    private void Update()
    {
        FadeManager.Instance.LerpFadeOut(_startPanelImage, .1f);
        if (_isFadeTime)
        {
            FadeInTexts();
        }
    }

    private void InitializePhase()
    {
        _startPanelImage = startPanel.GetComponent<Image>();
    }

    private void FadeInTexts()
    {
        foreach (var text in introTexts)
        {
            FadeManager.Instance.FadeIn(text, .05f);
        }
    }

    private void FadeDelay()
    {
        _isFadeTime = true;
    }
}
