using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyDamage : MonoBehaviour
{
    public float damage;
    public SC_RigidbodyPlayerMovement player;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.takeDamage(damage);
        }
    }
}
