using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FlockAgentV2 : MonoBehaviour
{
    SphereCollider m_BoxCollider;
    public SphereCollider agentCollider {  get { return m_BoxCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider = GetComponent<SphereCollider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward= velocity;
        transform.position += velocity *Time.deltaTime;
    }
}
