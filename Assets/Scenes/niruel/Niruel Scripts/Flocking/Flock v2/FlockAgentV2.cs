using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlockAgentV2 : MonoBehaviour
{
    SphereCollider m_SphereCollider;
    public SphereCollider agentCollider {  get { return m_SphereCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward= velocity;
        transform.position += velocity *Time.deltaTime;
    }
}
