using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
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
    SC_RigidbodyPlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        spideranim = GetComponentInChildren<spiderAnimation>();
        m_SphereCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        var playerObj = GameObject.Find("First Person Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<SC_RigidbodyPlayerMovement>();
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
        SurrAlign();
         
    }

    private float cooldownTime = 5f;
    private float nextTime = 0;
    private void Update()
    {
       
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= m_agentFlock.neighborRadis )
        {
            transform.LookAt(player.transform.position);
            if (distance < 3f && Time.time >= nextTime)
            {
                spideranim.isAttacking = true;
                player.takeDamage(4);
                nextTime = Time.time + cooldownTime;
            }
            else
            {
                spideranim.isAttacking = false;
            }

        }
       
    }
    void SurrAlign()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(transform.position, -Vector3.forward, Color.magenta);
        RaycastHit info = new RaycastHit();
        if (Physics.SphereCast(transform.position, .5f,-Vector3.down,out info))
        {
            transform.up += info.transform.up.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up,info.normal);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - Vector3.up, .5f);
    }


}
