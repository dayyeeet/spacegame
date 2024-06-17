using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovementEvent : MonoBehaviour
{
    public event Action<SpaceShipMovementEvent, SpaceShipMovementArgs> OnSpaceshipMovement;

    public void CallOnSpaceshipMovement(float verticalInput, float horizontalInput, float hoverInput, float rollInput)
    {
        OnSpaceshipMovement?.Invoke(this, new SpaceShipMovementArgs()
        {
            verticalInput = verticalInput,
            horizontalInput = horizontalInput,
            hoverInput = hoverInput,
            rollInput = rollInput
        });
    }
}

public class SpaceShipMovementArgs : EventArgs
{
    public float verticalInput;
    public float horizontalInput;
    public float hoverInput;
    public float rollInput;
}