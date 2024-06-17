using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
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

        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContex =  (Filter == null) ? context : Filter.filter(agent, context);
        foreach (Transform t in filteredContex)
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
