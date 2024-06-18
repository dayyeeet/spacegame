using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Pursue")]
public class PursueBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float agentSmoothTime = .5f;
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            //Debug.Log("g");
            return Vector3.zero;
        }

        Vector3 moveToPlayer = Vector3.zero;
        Vector3 moveFromPlayer = Vector3.zero;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        //Debug.Log(filteredContex);
        foreach (Transform t in filteredContex)
        {
            
            //Vector3 closestPoint = t.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            Debug.DrawLine(agent.transform.position, t.position,Color.red);
            float distance = Vector3.Distance(agent.transform.position, t.position);
            //Debug.Log(distance);
            if (distance > 1.2f)
            {
               moveToPlayer = t.position - agent.transform.position;
                moveToPlayer = Vector3.SmoothDamp(agent.transform.forward, moveToPlayer, ref currentVelocity, agentSmoothTime);
            }
            
           
        }
        return moveToPlayer;

    }
}
