using UnityEngine;

public class CriticalStaminaBar : MonoBehaviour
{
    public StatusBar statusBar;
    public GameObject gameOver;

    private void Update()
    {
        if (statusBar.currentValue <= 0)
        {
            gameOver.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
