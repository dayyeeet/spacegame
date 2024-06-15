using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgentV2 agentPrefab;
    List<FlockAgentV2> agents = new List<FlockAgentV2>();
    public FlockBehavior behavior;

    public int startCount = 25;
    //make const latter
    [Range(0f,1f)]
    [SerializeField]  float agentDensity = .1f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f,100f)]
    public float maxSpeed = 5f;

    [Range(1f, 100f)]
    public float neighborRadis = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float m_squareAviodanceRadius;
    public float squareAviodanceRadius { get { return m_squareAviodanceRadius;} }


    float SqaureTheValue(float value)
    {
        return value*value;
    }
    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed= SqaureTheValue(maxSpeed);
        squareNeighborRadius = SqaureTheValue(neighborRadis);
        m_squareAviodanceRadius = squareNeighborRadius* SqaureTheValue(avoidanceRadiusMultiplier);
        
        for (int i = 0; i < startCount; i++)
        {
            Vector2 randCirclePos = Random.insideUnitCircle * startCount * agentDensity;
            Vector3 randPoint = transform.position + new Vector3(randCirclePos.x, 0, randCirclePos.y);
            FlockAgentV2 newAgent = Instantiate(
                agentPrefab,
                randPoint,
                Quaternion.Euler(Vector3.up* Random.Range(0f,360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
