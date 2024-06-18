using UnityEngine;

public class SpaceshipLandingProcess : MonoBehaviour
{
    [SerializeField] private Planet planetGameObject;
    [SerializeField] private float landingSpeed;

    private SC_PlanetGravity _gravity;
    private GetInAndOutSpaceship _getInAndOutController;
    private Vector3 _landingPoint;

    private bool _canShipLand;
    private bool _isLanding;

    private void Start()
    {
        _gravity = GetComponent<SC_PlanetGravity>();
        _gravity.enabled = false;

        _getInAndOutController = GetComponentInChildren<GetInAndOutSpaceship>();
    }

    private void Update()
    {
        if (_isLanding)
        {
            return;
        }
        
        StartLanding();
        
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit,100.0f);

        if (hit.collider.gameObject)
        {
            var planetToLand = GetComponentFromAnyParent<Planet>(hit.collider.gameObject);
            _gravity.planet = planetToLand;
            _canShipLand = planetToLand != null;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (_gravity.enabled)
        {
            _gravity.enabled = false;
            _isLanding = false;
            StartCoroutine(_getInAndOutController.GetOutSpaceship());
        }
    }

    private void StartLanding()
    {
        if (_canShipLand && Input.GetKeyDown(KeyCode.L))
        {
            _isLanding = true;
            _gravity.enabled = true;
        }
        
    
        // if (transform.position != _landingPoint)
        // {
        //     var direction = _landingPoint - transform.position;
        //     transform.Translate(direction * (Time.deltaTime * landingSpeed));
        // }
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
