using UnityEngine;

public class PlayerSound : SoundManager
{
    public static PlayerSound Instance;
    

    private void Awake()
    {
        Instance = this;
    }

    public bool isTakeDamage;
    
    private SC_RigidbodyPlayerMovement _scRigidbodyPlayerMovement;
    private AudioSource[] _audioSources;
    public Spaceship spaceship;

    private void Start()
    {
        _scRigidbodyPlayerMovement = GetComponent<SC_RigidbodyPlayerMovement>();
        _audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (spaceship.isPlayerInSpaceship) return;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            _audioSources[0].Play();
        }

        if (isTakeDamage)
        {
            _audioSources[2].Play();
            isTakeDamage = false;
        }
    }
}
