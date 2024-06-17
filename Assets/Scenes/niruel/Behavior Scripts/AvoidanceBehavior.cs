using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Avoidence")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
   
   
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        foreach (Transform t in filteredContex)
        {
            Vector3 closestPoint = t.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position)< flock.squareAviodanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position- closestPoint;
            }
            
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

       
        return avoidanceMove;
    }
}
