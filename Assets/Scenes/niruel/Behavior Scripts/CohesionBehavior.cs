using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            
            return Vector3.zero;
        }

        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform t in context)
        {
            cohesionMove += t.position;
        }
        cohesionMove /= context.Count;

        //create offset
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }

   
}
