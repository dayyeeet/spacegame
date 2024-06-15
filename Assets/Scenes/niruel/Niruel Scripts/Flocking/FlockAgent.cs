using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    [SerializeField]float speed; 
    [SerializeField]bool turning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range( FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        //Bounds bounds = new Bounds(FlockManager.instance.transform.position,  new Vector3 (10,1 ,10) * 2);
        //if (transform.position.y < 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        //}
        //if (!bounds.Contains(transform.position))
        //{
        //    turning = true;
        //}
        //else
        //{
        //    turning = false;
        //}
        //if (turning)
        //{
        //    Vector3 direction = FlockManager.instance.transform.position - this.transform.position;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.instance.rotationSpeed * Time.deltaTime);
        //}
       // else
        {
            if (Random.Range(0, 100) < 10)
            {
                speed = Random.Range(FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
            }

            if (Random.Range(0, 100) < 20)
            {
                ApplyRules();
            }
        }
      
       
        this.transform.Translate((Vector3.forward * speed )* Time.deltaTime);
    }

    void ApplyRules()
    {
        GameObject[] agents = FlockManager.instance.allAgents;
        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float groupSpeed = 0.01f;
        float neighbourDistance;
        int groupSize = 0;

        foreach (GameObject agent in agents) 
        {
            if (agent != this.gameObject)
            {

                neighbourDistance = Vector3.Distance(agent.transform.position, this.transform.position);
                
                if (neighbourDistance <= FlockManager.instance.neighbourDistance)
                {
                    Debug.DrawRay(this.transform.localPosition, Vector3.forward, Color.blue);
                    vCenter += agent.transform.position;
                    groupSize++;

                    if (neighbourDistance <2f)
                    {
                        Debug.LogWarning($"the distane between neighbours: {neighbourDistance}");
                        vAvoid = vAvoid + (this.transform.position - agent.transform.position);
                        

                    }
                    FlockAgent anotherFlockAgent = agent.GetComponent< FlockAgent >();
                    groupSpeed = groupSpeed + anotherFlockAgent.speed;
                }
            }
        }
        if (groupSize > 0)
        {
            Debug.Log($"thre group size: {groupSize}");
            vCenter = vCenter / groupSize + (FlockManager.instance.goalPosition - this.transform.position);
            speed = groupSpeed / groupSize;
            if (speed > FlockManager.instance.maxSpeed)
            {
                speed = FlockManager.instance.maxSpeed;
            }
            Vector3 direction = (vCenter + vAvoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 
                                                        FlockManager.instance.rotationSpeed * Time.deltaTime);
            }
        }
    }

    //public static Vector3 GetRandomDirection()
    //{
    //    return new Vector3(Random.Range(-1f,1f),0f,Random.Range(-1f,1f)).normalized;
    //}
}
