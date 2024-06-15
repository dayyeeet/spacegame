using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderAnimation : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking;
    public bool isWalking;
    public bool isIdle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isAttacking)
        {
            isWalking = false;

            animator.SetBool("run", false);
            animator.SetBool("attack", true);
        }
        else if(isWalking)
        {
            isAttacking = false;

            animator.SetBool("run", true);
            animator.SetBool("attack", false);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
        }

    }
}
