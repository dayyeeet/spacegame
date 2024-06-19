using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyAlienAI : MonoBehaviour
{
    SphereCollider perceptionCol;
    [SerializeField] float speed;
    GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        if (obj == null)
        {
            obj = GameObject.Find("Player");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.forward * 10f * Time.deltaTime;
        //transform.position += Vector3.forward * 1f * Time.deltaTime;
        
        if (obj != null)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance > 2f)
            {
                Debug.Log(distance);
                transform.position += transform.forward * 1f *Time.deltaTime;
                
            }
           
           

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Player")
        {
            obj = other.gameObject;
            Debug.Log(other.gameObject.name);
            
            //float distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
            //if(distance > .1f)
            {
                
                transform.LookAt(other.gameObject.transform.position);
                

            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        transform.position -= collision.gameObject.transform.position;
    }
}
