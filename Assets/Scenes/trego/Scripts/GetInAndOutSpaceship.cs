using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetInAndOutSpaceship : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Camera spaceshipCamera;
    [SerializeField] private bool isSpaceShipOnLand;
    [SerializeField] private GameObject planetGameObject;
    
    private Spaceship _spaceship;
    // private Rigidbody _spaceshipRigidbody;
    
    private SkinnedMeshRenderer _playerMesh;
    private SC_RigidbodyPlayerMovement _playerController;
    private Camera _playerCamera;

    private void Start()
    {
        _spaceship = GetComponentInParent<Spaceship>();
        
        _playerMesh = playerGameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        _playerCamera = playerGameObject.GetComponentInChildren<Camera>();
        _playerController = playerGameObject.GetComponent<SC_RigidbodyPlayerMovement>();
    }

    private void Update()
    {
        isSpaceShipOnLand =
            (planetGameObject.transform.position - _spaceship.gameObject.transform.position).magnitude <= 55;
        // Debug.Log((planetGameObject.transform.position - _spaceship.gameObject.transform.position).magnitude);
        StartCoroutine(GetInSpaceship());
        if (_spaceship.isPlayerInSpaceship && Input.GetKeyDown(KeyCode.F) && isSpaceShipOnLand)
        {
            StartCoroutine(GetOutSpaceship());
        }
        
    }

    private IEnumerator GetInSpaceship()
    {
        if ((playerGameObject.transform.position - transform.position).magnitude <= 15 && Input.GetKeyDown(KeyCode.F))
        {
            yield return new WaitForSeconds(0.2f);

            _spaceship.isPlayerInSpaceship = true;
            spaceshipCamera.gameObject.SetActive(true);
            
            _playerMesh.enabled = false;
            _playerController.enabled = false;
            _playerCamera.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public IEnumerator GetOutSpaceship()
    {
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

        playerGameObject.transform.position = _spaceship.transform.localPosition + new Vector3(5, 0);
            
        Cursor.lockState = CursorLockMode.Locked;
    }
}
