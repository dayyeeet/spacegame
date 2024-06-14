using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public static FlockManager instance;

    [SerializeField] GameObject agentPrefab;
    [SerializeField] GameObject playerTransform;
    [SerializeField] int numberOfAgents = 5;
     public GameObject[] allAgents;
     public Vector3 goalPosition = Vector3.zero;
     public Vector3 agentMovementLimits = new Vector3(5,0,5);
    

    [Header("Agent Setting")]
    [Range(0f, 5f)]
    [SerializeField] public float minSpeed;

    [Range(0f, 5f)]
    [SerializeField] public float maxSpeed;

    [Range(1f, 10f)]
    [SerializeField] public float neighbourDistance;

    [Range(1f, 5f)]
    [SerializeField] public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        allAgents = new GameObject[numberOfAgents];
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 startPos = this.transform.position + new Vector3(Random.Range(-agentMovementLimits.x,agentMovementLimits.x),agentMovementLimits.y, 
                                                                        Random.Range(-agentMovementLimits.z, agentMovementLimits.z));

            allAgents[i] = Instantiate(agentPrefab, startPos,Quaternion.identity);

        }
        instance = this;
        if (playerTransform != null)
        {
            goalPosition = playerTransform.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            goalPosition = playerTransform.transform.position;
            Debug.Log($"the goal position: {goalPosition}");
        }
    }
}
