using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private float shakeDelay;
    [SerializeField] private float shakeInterval;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 5f;
    
    private Vector3 _originalPosition;

    void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void Start()
    {
        InvokeRepeating(nameof(StartShake), shakeDelay, shakeInterval);
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = shakeMagnitude * (1f - (elapsedTime / shakeDuration));
            transform.localPosition = _originalPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = _originalPosition; // Reset to original position
    }
}

