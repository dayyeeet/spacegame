using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{

    Vector3 currentVelocity;
    public float agentSmoothTime = .5f;
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
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
