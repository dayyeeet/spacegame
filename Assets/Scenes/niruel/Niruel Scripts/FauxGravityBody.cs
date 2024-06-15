using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    Rigidbody rb

;    //Transform agentTransform;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false;
            rb.transform.position = transform.position;
        }
        else
        {
            Debug.LogError($"the rb is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
       FlockManager.instance.gravityAttractor.Attract(rb);
    }
}
