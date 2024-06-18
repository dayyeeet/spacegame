using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            
            return Vector3.zero;
        }

        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        foreach (Transform t in filteredContex)
        {
            cohesionMove += t.position;
        }
        cohesionMove /= context.Count;

        //create offset
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }

   
}
