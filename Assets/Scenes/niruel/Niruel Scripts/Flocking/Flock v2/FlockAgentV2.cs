using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FlockAgentV2 : MonoBehaviour
{
    BoxCollider m_BoxCollider;
    public BoxCollider agentCollider {  get { return m_BoxCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    public void Move(Vector3 velocity)
    {
        transform.forward= velocity;
        transform.position += velocity *Time.deltaTime;
    }
}
