using UnityEngine;

public class SC_PlanetGravity : MonoBehaviour
{
    public Planet planet;
    public bool alignToPlanet = true;
    Rigidbody r;


    void Start()
    {
        r = GetComponent<Rigidbody>();
        if (planet == null)
        {
            planet = GameObject.FindAnyObjectByType<Planet>();
            if (planet != null)
            {
                
            }
        }
    }

    void Update()
    {
        Vector3 toCenter = planet.gravityObject.transform.position - transform.position;
        toCenter.Normalize();


        r.AddForce(toCenter * planet.gravityConstant, ForceMode.Acceleration);

        if (alignToPlanet)
        {
            Quaternion q = Quaternion.FromToRotation(transform.up, -toCenter);
            q *= transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }
    }
}