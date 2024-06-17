using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovementEvent : MonoBehaviour
{
    public event Action<SpaceShipMovementEvent, SpaceShipMovementArgs> OnSpaceshipMovement;

    public void CallOnSpaceshipMovement(Vector3 position, float verticalInput)
    {
        OnSpaceshipMovement?.Invoke(this, new SpaceShipMovementArgs()
        {
            position = position,
            verticalInput = verticalInput
        });
    }
}

public class SpaceShipMovementArgs : EventArgs
{
    public Vector3 position;
    public float verticalInput;
}