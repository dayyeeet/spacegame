using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Canvas inGameMenuCanvas;
    [SerializeField] private AudioSource buttonClickSound;
    
    // Will be open if settings panel be designed
    // [SerializeField] private GameObject settingsPanel;
    
    public void Resume()
    {
        buttonClickSound.Play();
        inGameMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    // Will be open if settings panel be designed
    // public void OpenSettingsPanel()
    // {
    //     settingsPanel.SetActive(true);
    // }

    public void BackToMainMenu()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("StartScene");
    }
}
