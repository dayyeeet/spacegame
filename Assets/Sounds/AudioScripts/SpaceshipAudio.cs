using UnityEngine;

public class SpaceshipAudio : SoundManager
{
    private Spaceship _spaceship;
    private AudioSource[] _audioSources;

    private void Start()
    {
        _spaceship = GetComponent<Spaceship>();
        _audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (_spaceship.isPlayerInSpaceship && Input.GetKeyDown(KeyCode.W))
        {
            _audioSources[0].Play();
        }
    }
}
