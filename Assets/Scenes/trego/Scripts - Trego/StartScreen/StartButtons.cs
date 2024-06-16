using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour
{
    private StartPanel _startPanelScript;
    private Image _image;
    private TextMeshProUGUI _buttonTexts;
    
    void Start()
    {
        _startPanelScript = GetComponentInParent<StartPanel>();
        _image = GetComponent<Image>();
        _buttonTexts = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        ButtonFadeIn();
    }

    private void ButtonFadeIn()
    {
        FadeManager.Instance.FadeIn(_image, .2f);
        FadeManager.Instance.FadeIn(_buttonTexts, .2f);
    }
}
