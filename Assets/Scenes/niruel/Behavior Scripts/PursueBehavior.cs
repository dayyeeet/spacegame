using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Pursue")]
public class PursueBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = .5f;
    float distance;

    Transform agentTrans;

    Quaternion q;
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        

        Vector3 Player = Vector3.zero;
        Vector3 moveToPlayer = Vector3.zero;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        if (filteredContex.Count == 0)
        {
            //Debug.Log("g");
            return Vector3.zero;
        }
        //Debug.Log(filteredContex);
        foreach (Transform t in filteredContex)
        { 
            //Vector3 closestPoint = t.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            Debug.DrawLine(t.position, agent.transform.position,Color.red);
            distance = Vector3.Distance(t.position, agent.transform.position);

            //if (distance > 3f /*1.2f*/)
            {
                //Debug.Log(distance);
                Player = t.position;
                agentTrans = t;
            }
        }
        Debug.Log($"context is {Player}");

        if (distance > 2f) 
        {
            moveToPlayer = Player-agent.transform.position;
           
        }
       
        return moveToPlayer;

    }
}
