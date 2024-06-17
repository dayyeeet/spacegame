using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseMenu : MonoBehaviour
{
    [SerializeField] private Canvas inGameMenuCanvas;
    
    private void Update()
    {
        OpenAndCloseMenuOnEscPressed();
    }

    private void OpenAndCloseMenuOnEscPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inGameMenuCanvas.gameObject.activeSelf)
        {
            Time.timeScale = 0;
            inGameMenuCanvas.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inGameMenuCanvas.gameObject.activeSelf)
        {
            Time.timeScale = 1;
            inGameMenuCanvas.gameObject.SetActive(false);
        }
    }
}
