using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgentV2 : MonoBehaviour
{
    Flock m_agentFlock;
    //FauxGravityAttractor m_gravityAttractor;
    public Flock agentFlock {  get { return m_agentFlock; } }
    Collider m_SphereCollider;
    public Collider agentCollider {  get { return m_SphereCollider; } }
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        m_SphereCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Init(Flock flock)
    {
        m_agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward= velocity;
        transform.position += velocity *Time.deltaTime;
    }

}
