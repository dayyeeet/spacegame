using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClickSound;
    public void BackToMenuButton()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("StartScene");
    }
}
