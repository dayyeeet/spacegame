using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaySound();
        }
    }

    public virtual void PlaySound()
    {
        _audioSource.Play();
    }
}
