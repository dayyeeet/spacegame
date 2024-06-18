using UnityEngine;
using UnityEngine.UI;

public class SpaceshipLandingProcess : MonoBehaviour
{
    [SerializeField] private Planet planetGameObject;
    [SerializeField] private GameObject landingUIText;

    private SC_PlanetGravity _gravity;
    private GetInAndOutSpaceship _getInAndOutController;
    private Rigidbody _spaceshipRigidbody;
    
    private bool _canShipLand;
    private bool _isLanding;

    private void Start()
    {
        _spaceshipRigidbody = GetComponent<Rigidbody>();
        
        _gravity = GetComponent<SC_PlanetGravity>();
        _gravity.enabled = false;

        _getInAndOutController = GetComponentInChildren<GetInAndOutSpaceship>();
    }

    private void Update()
    {
        if (_isLanding)
        {
            landingUIText.SetActive(false);
            return;
        }
        
        StartLanding();
        
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,100.0f);

        if (hit.collider != null && hit.collider.gameObject)
        {
            var planetToLand = GetComponentFromAnyParent<Planet>(hit.collider.gameObject);
            _gravity.planet = planetToLand;
            _canShipLand = planetToLand != null;
            landingUIText.SetActive(true);
        }
        else if (hit.collider == null)
        {
            _canShipLand = false;
            landingUIText.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (_gravity.enabled)
        {
            _spaceshipRigidbody.isKinematic = true;
            _gravity.enabled = false;
            _isLanding = false;
            StartCoroutine(_getInAndOutController.GetOutSpaceship());
        }
    }

    private void StartLanding()
    {
        if (_canShipLand && Input.GetKeyDown(KeyCode.L))
        {            
            landingUIText.SetActive(false);
            _isLanding = true;
            _gravity.enabled = true;
        }
    }
    
    T GetComponentFromAnyParent<T>(GameObject child) where T : Component
    {
        Transform currentParent = child.transform.parent;

        while (currentParent != null)
        {
            T component = currentParent.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            currentParent = currentParent.parent;
        }

        return null;
    }
}
