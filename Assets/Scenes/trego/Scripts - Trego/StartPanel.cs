using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    public bool isShrinkEnd;
    
    [SerializeField] private string nameOfSceneToLoad;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI gameNameReference;
    [SerializeField] private TextMeshProUGUI pressSpaceText;
    [SerializeField] private  StartButtons[] startButtons;
    
    private bool _isSpacePressed = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        IntroManager.Instance.isFadeTime = false;
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
            _isSpacePressed = false;
        }
    }

    private IEnumerator ActivateButtons()
    {
        foreach (var button in startButtons)
        {
            button.gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
    }
    
    public void StartButton()
    {
        SceneManager.LoadScene($"{nameOfSceneToLoad}");
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
