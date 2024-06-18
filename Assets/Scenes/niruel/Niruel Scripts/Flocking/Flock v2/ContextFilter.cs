using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
   public abstract List<Transform> filter(FlockAgentV2 agent, List<Transform> original);
}
