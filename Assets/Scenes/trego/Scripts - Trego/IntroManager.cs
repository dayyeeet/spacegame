using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    #region Singleton

    public static IntroManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    public bool isFadeTime;
    
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI pressSpaceText;

    private Image _startPanelImage;
    
    
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
        
        if (isFadeTime)
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
        FadeManager.Instance.FadeIn(gameNameText, .1f);
        FadeManager.Instance.FadeIn(pressSpaceText, .05f);
    }

    private void FadeDelay()
    {
        isFadeTime = true;
    }
}
