using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SpaceshipParticleController : MonoBehaviour
{
    private ParticleSystem particleSystemObj;

    void OnEnable()
    {
        SpaceShipMovementEvent spaceshipMovementEvent = this.transform.parent.parent.GetComponent<SpaceShipMovementEvent>();
        if (spaceshipMovementEvent is null)
        {
            Debug.LogError("Parent of a parent doesn't have SpaceShipMovementEvent class");
            return;
        }
        spaceshipMovementEvent.OnSpaceshipMovement += OnSpaceShipMovementEvent_ControlParticle;
    }

    void Awake()
    {
        this.particleSystemObj = this.GetComponent<ParticleSystem>();
    }

    private void OnSpaceShipMovementEvent_ControlParticle(SpaceShipMovementEvent spaceShipMovementEvent, SpaceShipMovementArgs spaceShipMovementArgs)
    {
        if (spaceShipMovementArgs.verticalInput > 0.1)
        {
            this.particleSystemObj.Play();
        }
        else
        {
            this.particleSystemObj.Stop();
        }
    }
}
