using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgentV2 agentPrefab;
    public Planet planet;
    List<FlockAgentV2> agents = new List<FlockAgentV2>();
    public FlockBehavior behavior;
    [SerializeField] public float YspawnPos =0f;
    public int startCount = 25;
    //make const latter
    //[Range(0f,1f)]
    const float agentDensity = .3f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f,100f)]
    public float maxSpeed = 5f;

    [Range(0f, 100f)]
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
        
        //Debug.Log($"the radius is {planet.shapeSettings.planetRadius}");
        squareMaxSpeed= SqaureTheValue(maxSpeed);
        squareNeighborRadius = SqaureTheValue(neighborRadis);
        m_squareAviodanceRadius = squareNeighborRadius* SqaureTheValue(avoidanceRadiusMultiplier);
        for (int i = 0; i < startCount; i++)
        {
            //Vector2 randCirclePos = Random.insideUnitCircle * startCount * agentDensity;
            Vector3 randV3 = new Vector3(Random.onUnitSphere.x, Random.onUnitSphere.y, Random.onUnitSphere.z) * 8;
            //Debug.Log($"the radius is {randV3}");
            Vector3 randPoint = randV3 * startCount * agentDensity;
            FlockAgentV2 newAgent = Instantiate(
                agentPrefab,
                randPoint,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            newAgent.Init(this);
            agents.Add(newAgent);
        }


        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (agents.Count<startCount*2)
        {
            yield return new WaitForSeconds(2f);
            // Vector2 randCirclePos = Random.insideUnitCircle * startCount * agentDensity;
            Vector3 randV3 = new Vector3(Random.onUnitSphere.x, Random.onUnitSphere.y, Random.onUnitSphere.z) * 10;
            Vector3 randPoint = randV3 * startCount * agentDensity;
            FlockAgentV2 newAgent = Instantiate(
                agentPrefab,
                randPoint,
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform
                );
            newAgent.transform.localPosition = randPoint;
            var gravity = newAgent.GetComponent<SC_PlanetGravity>();
            if (gravity != null)
            {
                gravity.planet = planet;
               
            }
            // newAgent.name = "Agent" + i;
            newAgent.Init(this);
            agents.Add(newAgent);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(this.transform.position);
        foreach (FlockAgentV2 agent in agents)
        {
            List<Transform> contex = GetNearbyObjects(agent);
            
            //Debug.DrawRay(agent.transform.position,Vector3.forward,Color.yellow);
            //agent.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, contex.Count / 6f);
            Vector3 move = behavior.CalculateMover(agent, contex, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }
    List<Transform> GetNearbyObjects(FlockAgentV2 agent)
    {
        List<Transform> contex = new List<Transform>();
        Collider[] ContexColliders = Physics.OverlapSphere(agent.transform.position, neighborRadis);
        foreach (Collider c in ContexColliders)
        {
            if (c != agent.agentCollider )
            {
                contex.Add(c.transform);
            }
        }
        return contex;

    }
}
