using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeEffect : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 5f;

    private Vector3 _originalPosition;

    void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void Start()
    {
        InvokeRepeating(nameof(StartShake), 1, 3);
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

