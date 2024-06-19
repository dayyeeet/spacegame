using System.Collections;
using UnityEngine;

public class GetInAndOutSpaceship : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Camera spaceshipCamera;
    [SerializeField] private bool isSpaceShipOnLand;

    private Planet _planet;
    private Spaceship _spaceship;
    private Rigidbody _spaceshipRigidbody;
    private SC_PlanetGravity _playerGravity;

    private SkinnedMeshRenderer _playerMesh;
    private SC_RigidbodyPlayerMovement _playerController;
    private Camera _playerCamera;

    private void Start()
    {
        _spaceship = GetComponentInParent<Spaceship>();
        _spaceshipRigidbody = GetComponentInParent<Rigidbody>();

        _playerMesh = playerGameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        _playerCamera = playerGameObject.GetComponentInChildren<Camera>();
        _playerGravity = playerGameObject.GetComponent<SC_PlanetGravity>();
        _playerController = playerGameObject.GetComponent<SC_RigidbodyPlayerMovement>();
        StartCoroutine(GetInSpaceship());
    }

    private void Update()
    {
        isSpaceShipOnLand = _planet != null &&
                            (_planet.transform.position - _spaceship.gameObject.transform.position).magnitude <= 55;
        // Debug.Log((planetGameObject.transform.position - _spaceship.gameObject.transform.position).magnitude);

        if ((playerGameObject.transform.position - transform.position).magnitude <= 15 && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(GetInSpaceship());
        }

        if (_spaceship.isPlayerInSpaceship && Input.GetKeyDown(KeyCode.F) && isSpaceShipOnLand)
        {
            StartCoroutine(GetOutSpaceship());
        }
    }

    public void SetPlanet(Planet planet)
    {
        _planet = planet;
    }

    private IEnumerator GetInSpaceship()
    {
        yield return new WaitForSeconds(0.2f);

        _spaceship.isPlayerInSpaceship = true;
        spaceshipCamera.gameObject.SetActive(true);
        _spaceshipRigidbody.isKinematic = false;
        _playerGravity.enabled = false;

        _playerMesh.enabled = false;
        _playerController.enabled = false;
        _playerCamera.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public IEnumerator GetOutSpaceship()
    {
        if (_planet == null) yield break;
        if (!_spaceship.isPlayerInSpaceship)
        {
            yield break;
        }
        
        yield return new WaitForSeconds(0.2f);
        
        _playerMesh.enabled = true;
        _playerController.enabled = true;
        _playerCamera.gameObject.SetActive(true);

        _spaceship.isPlayerInSpaceship = false;
        spaceshipCamera.gameObject.SetActive(false);

        _playerGravity.planet = _planet;
        _playerGravity.enabled = true;
        playerGameObject.transform.position = _spaceship.transform.localPosition + new Vector3(5, 0);

        Cursor.lockState = CursorLockMode.Locked;
    }
}