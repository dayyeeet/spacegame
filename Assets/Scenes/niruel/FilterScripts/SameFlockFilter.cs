using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> filter(FlockAgentV2 agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            FlockAgentV2 itemAgent = item.GetComponent< FlockAgentV2>();

            if (itemAgent != null && itemAgent.agentFlock == agent.agentFlock)
            {
                filtered.Add(item);
            }
            
        }
        return filtered;
    }

   
}
