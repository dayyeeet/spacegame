using UnityEngine;

public class SC_PlayerLight : MonoBehaviour
{
    public Light playerSpotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;
    private float intensityVelocity;
    private readonly float smoothTime = 0.1f;

    private float targetIntensity;

    private void Start()
    {
        if (playerSpotlight == null) playerSpotlight = GetComponentInChildren<Light>();
        targetIntensity = playerSpotlight.intensity;
    }

    private void Update()
    {
        // Ensure the light's position matches the camera's position
        playerSpotlight.transform.position = transform.position;

        // Ensure the light's direction matches the camera's direction
        playerSpotlight.transform.rotation = transform.rotation;

        // Flickering effect
        if (Random.value > 0.9f) // Randomly decide whether to change the intensity
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        playerSpotlight.intensity =
            Mathf.SmoothDamp(playerSpotlight.intensity, targetIntensity, ref intensityVelocity, smoothTime);
    }
}