using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Animator animator;

    private SC_RigidbodyPlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<SC_RigidbodyPlayerMovement>();
        Debug.Log(animator);
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.returnState() == SC_RigidbodyPlayerMovement.MovementState.sprinting)
        {
            animator.SetBool("isSprinting", true);
        }

        if (player.returnState() != SC_RigidbodyPlayerMovement.MovementState.sprinting)
        {
            animator.SetBool("isSprinting", false);
        }
            
        if (Input.GetButton("Vertical") && player.returnState() != SC_RigidbodyPlayerMovement.MovementState.sprinting)
        {
            animator.SetBool("isWalking", true);
        }
        
        if (!Input.GetButton("Vertical"))
        {
            animator.SetBool("isWalking", false);
        }
    }
}
