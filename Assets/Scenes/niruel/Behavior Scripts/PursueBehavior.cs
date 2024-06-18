using System.Collections;
using System.Collections.Generic;
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

        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContex = (Filter == null) ? context : Filter.filter(agent, context);
        //Debug.Log(filteredContex);
        foreach (Transform t in filteredContex)
        {
            //Vector3 closestPoint = t.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            Debug.DrawLine(agent.transform.position, t.position,Color.red);
            if (Vector3.Distance(agent.transform.position, t.position) > 1f)
            {
                cohesionMove = t.position - agent.transform.position;
                cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
            }

            Debug.Log(t.name);
        }
        //Debug.Log(cohesionMove);
        //cohesionMove /= context.Count;

        ////create offset
        //cohesionMove -= agent.transform.position;
        
        return cohesionMove;

    }
    }
