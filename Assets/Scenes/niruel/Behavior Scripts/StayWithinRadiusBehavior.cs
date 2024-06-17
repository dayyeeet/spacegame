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
       
       Vector3 centerOffset = centerPoint-agent.transform.position;
        //Debug.Log(centerOffset);
        float t = centerOffset.magnitude/radius;
        Debug.Log(t);

        if (t < 0.9f)
        {  
            return Vector3.zero;
        }
        return centerOffset * t*t;
    }
}
