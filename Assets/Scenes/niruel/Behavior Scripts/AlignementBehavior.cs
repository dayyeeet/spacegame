using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignementBehavior : FlockBehavior
{
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        Vector3 alignmentnMove = Vector3.zero;
        foreach (Transform t in context)
        {
            alignmentnMove += t.transform.forward;
        }
        alignmentnMove /= context.Count;

        //create offset
        //cohesionMove -= agent.transform.position;
        return alignmentnMove;
    }
}
