using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay in Radius")]
public class StayWithinRadiusBehavior : FlockBehavior
{
    public Vector3 centerPoint;
    public float radius = 15f;
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
       
       //Debug.DrawLine(agent.transform.position, centerPoint, Color.red);
       Vector3 centerOffset = centerPoint-agent.transform.position;
        
        float t = centerOffset.magnitude/radius;

        if (t < 0.9f)
        {  
            return Vector3.zero;
        }
        if ( context.Count==0)
        {
           
            return Vector3.zero;
        }
        Vector3 d = centerOffset *t *t ;
       
        return d;
    }
}
