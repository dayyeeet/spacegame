using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

public class FadeManager : MonoBehaviour
{
    #region Singleton

    public static FadeManager Instance;

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
    
    public void FadeIn(TextMeshProUGUI textToFade, float fadeSpeed)
    {
        var color = textToFade.color;
        color.a += fadeSpeed * Time.deltaTime;
        textToFade.color = color;
    }

    public void FadeIn(Button buttonToFade, float fadeSpeed)
    {
        var color = buttonToFade.image.color;
        color.a += fadeSpeed * Time.deltaTime;
        buttonToFade.image.color = color;
    }
    
    public void FadeIn(Image imageToFade, float fadeSpeed)
    {
        var color = imageToFade.color;
        color.a += fadeSpeed * Time.deltaTime;
        imageToFade.color = color;
    }
    
    public void FadeOut(TextMeshProUGUI textToFade, float fadeSpeed)
    {
        var color = textToFade.color;
        color.a -= fadeSpeed * Time.deltaTime;
        textToFade.color = color;
    }
    
    public void FadeOut(Image imageToFade, float fadeSpeed)
    {
        var color = imageToFade.color;
        color.a -= fadeSpeed * Time.deltaTime;
        imageToFade.color = color;
    }
    
    public void LerpFadeOut(Image imageToFade, float fadeSpeed)
    {
        var color = imageToFade.color;
        color.a = Mathf.Lerp(color.a, 0, fadeSpeed * Time.deltaTime);
        imageToFade.color = color;
    }
}
