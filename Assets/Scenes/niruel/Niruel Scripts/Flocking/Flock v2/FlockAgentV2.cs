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
    //public Rigidbody rb;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spideranim = GetComponentInChildren<spiderAnimation>();
        m_SphereCollider = GetComponent<Collider>();
        //rb = GetComponent<Rigidbody>();
        player = GameObject.Find("First Person Playerg");
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
        transform.right= velocity;
        //Debug.Log(velocity);
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
            if(hit.collider.gameObject.name == "gravity")
            {
                //transform.position = new Vector3(0, agentFlock.YspawnPos, 0);
            }
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
       
        if (distance<3f )
        {
               

          
            transform.LookAt(player.transform.position);
            spideranim.isAttacking = true;
        }
        else
        {
            spideranim.isAttacking = false;
        }
        
    }


}
