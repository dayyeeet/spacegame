using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public bool isShrinkEnd;
    
    [SerializeField] private string nameOfSceneToLoad;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI gameNameReference;
    [SerializeField] private TextMeshProUGUI pressSpaceText;
    [SerializeField] private  StartButtons[] startButtons;
    [SerializeField] private  AudioSource buttonClickSound;
    [SerializeField] private  TMP_InputField seedInputField;
    
    private bool _isSpacePressed = false;
    private bool _isOnCreditsPage = false;
    
    private void Update()
    {
        CloseCreditsPanel();
        
        if (Input.GetKeyDown(KeyCode.Space) && IntroManager.Instance.isFadeTime)
        {
            _isSpacePressed = true;
        }

        if (_isSpacePressed)
        {
            OpeningSequence();
        }
    }
    
    public void OpeningSequence()
    {
        // Re-locate game name text
        MoveAndShrink(gameNameText.gameObject, gameNameReference.gameObject, 1.5f);
        
        // Dissolve pressSpace text
        FadeManager.Instance.FadeOut(pressSpaceText, .3f);
    }
    
    private void MoveAndShrink(GameObject baseObject, GameObject targetObject, float speed)
    {
        if (targetObject.transform.position.y - baseObject.transform.position.y > 1.6f)
        {
            baseObject.transform.position = Vector3.Lerp(baseObject.transform.position, targetObject.transform.position,
                speed * Time.deltaTime);
            
            baseObject.transform.localScale = Vector3.Lerp(baseObject.transform.localScale, targetObject.transform.localScale,
                speed * Time.deltaTime);
        }
        else
        {
            // Activate buttons when shrink stop
            StartCoroutine(ActivateButtons());
            
            IntroManager.Instance.isFadeTime = false;
            _isSpacePressed = false;
        }
    }

    private IEnumerator ActivateButtons()
    {
        seedInputField.gameObject.SetActive(true);
        
        foreach (var button in startButtons)
        {
            button.gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
    }
    
    public void StartButton()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene($"{nameOfSceneToLoad}");
    }

    // Will be activated when settings panel be ready
    public void CreditsButton()
    {
        buttonClickSound.Play();
        creditsPanel.SetActive(true);
        foreach (var button in startButtons)
        {
            button.gameObject.SetActive(false);
        }

        _isOnCreditsPage = true;
    }

    private void CloseCreditsPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isOnCreditsPage)
        {
            creditsPanel.SetActive(false);
            foreach (var button in startButtons)
            { 
                button.gameObject.SetActive(true);
            }

            _isOnCreditsPage = false;
        }
    }

    public void QuitButton()
    {
        buttonClickSound.Play();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
