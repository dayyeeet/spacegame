using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    float speed; 
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range( FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100)< 10)
        {
            speed = Random.Range(FlockManager.instance.minSpeed, FlockManager.instance.maxSpeed);
        }

        if (Random.Range(0, 100) < 10)
        {
            ApplyRules();
        }
       
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRules()
    {
        GameObject[] agents = FlockManager.instance.allAgents;
        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float groupSpeed = 0.01f;
        float neighbourDistance;
        int groupSize = 0;

        foreach (GameObject agent in agents) {
            if (agent != this.gameObject)
            {
                neighbourDistance = Vector3.Distance(agent.transform.position, this.transform.position);
                if (neighbourDistance <= FlockManager.instance.neighbourDistance)
                {
                    vCenter += agent.transform.position;
                    groupSize++;
                    if (neighbourDistance <1f)
                    {
                        vAvoid = vAvoid + (this.transform.position - agent.transform.position);

                    }
                    FlockAgent anotherFlockAgent = agent.GetComponent< FlockAgent >();
                    groupSpeed = groupSpeed +anotherFlockAgent.speed;
                }
            }
        }
        if (groupSize > 0)
        {
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
}
