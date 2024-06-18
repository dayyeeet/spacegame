using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignementBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
           
            return Vector3.forward;
        }

        Vector3 alignmentnMove = Vector3.zero;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        foreach (Transform t in filteredContex)
        {
            alignmentnMove += t.transform.forward;
        }
        alignmentnMove /= context.Count;

        //create offset
        //cohesionMove -= agent.transform.position;
        return alignmentnMove;
    }
}
