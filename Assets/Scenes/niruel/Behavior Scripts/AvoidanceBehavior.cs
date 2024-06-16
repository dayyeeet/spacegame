using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Avoidence")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        foreach (Transform t in context)
        {
            if (Vector3.SqrMagnitude(t.position - agent.transform.position)< flock.squareAviodanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position- t.position;
            }
            
        }
        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

       
        return avoidanceMove;
    }
}
