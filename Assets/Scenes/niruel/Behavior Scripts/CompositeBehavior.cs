using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{

    public FlockBehavior[] behaviors;
    public float[] weights;
    public override Vector3 CalculateMover(FlockAgentV2 agent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError($" Data miss match {name}, the weight behavior length {weights.Length}, is not equal to behaviors length {behaviors.Length} ", this);
            return Vector3.zero;    
        }

        //setup move
        Vector3 move = Vector3.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            //Debug.Log(behaviors[i].name);
            Vector3 partialMove = behaviors[i].CalculateMover(agent, context, flock) * weights[i];

            if (partialMove != Vector3.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                //else
                
                    move += partialMove;
                
            }
        }
        return move;
    }

   
}
