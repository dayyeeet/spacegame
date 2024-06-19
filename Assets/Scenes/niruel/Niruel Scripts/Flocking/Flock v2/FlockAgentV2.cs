using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgentV2 : MonoBehaviour
{
    Flock m_agentFlock;
    spiderAnimation spideranim;
    //FauxGravityAttractor m_gravityAttractor;
    public Flock agentFlock {  get { return m_agentFlock; } }
    Collider m_SphereCollider;
    public Collider agentCollider {  get { return m_SphereCollider; } }
    public Rigidbody rb;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spideranim = GetComponentInChildren<spiderAnimation>();
        m_SphereCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("First Person Player");
        if (player != null)
        {
            //Debug.Log($"player is here with {player.name}");
        }
    }

    public void Init(Flock flock)
    {
        m_agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward= velocity;
        spideranim.isWalking = true;
        transform.position += velocity *Time.deltaTime;
         
    }
    int attackCount = 0;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,-Vector3.up,out hit,3f))
        {
            //Debug.Log(hit.collider.gameObject.name);
            transform.up = hit.transform.up;
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance<3f )
        {
               

            Debug.Log($"the distance {distance}");
            transform.LookAt(player.transform.position);
            spideranim.isAttacking = true;
            Debug.Log($"the attack count {attackCount}");
        }
        else
        {
            spideranim.isAttacking = false;
        }
    }


}
