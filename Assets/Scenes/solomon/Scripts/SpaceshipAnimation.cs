using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAnimation : MonoBehaviour
{
    
    [Tooltip("Vertical")]
    [Range(0.5f, 5f)]
    [SerializeField] private float maxXLocalRotation = 1f;

    [Range(0.5f, 90f)]
    [Tooltip("Vertical")]
    [SerializeField] private float XSpeedRotation = 2f;

    [Tooltip("Horizontal")]
    [Range(1f, 90f)]
    [SerializeField] private float maxZLocalRotation = 18f;

    [Range(1f, 90f)]
    [Tooltip("Horizontal")]
    [SerializeField] private float ZSpeedRotation = 20f; 

    void OnEnable()
    {
        SpaceShipMovementEvent spaceshipMovementEvent = this.transform.parent.GetComponent<SpaceShipMovementEvent>();
        if (spaceshipMovementEvent is null)
        {
            Debug.LogError("Parent doesn't have SpaceShipMovementEvent class");
            return;
        }
        spaceshipMovementEvent.OnSpaceshipMovement += OnSpaceShipMovementEvent_DoAnimation;
    }

    private void OnSpaceShipMovementEvent_DoAnimation(SpaceShipMovementEvent spaceShipMovementEvent, SpaceShipMovementArgs spaceShipMovementArgs)
    {
        if(spaceShipMovementArgs.verticalInput != 0)
        {

        }
        if(spaceShipMovementArgs.hoverInput != 0)
        {

        }
        if(spaceShipMovementArgs.rollInput != 0)
        {

        }

        float xRotation = (spaceShipMovementArgs.verticalInput != 0) ? (this.maxXLocalRotation * Mathf.Sign(spaceShipMovementArgs.verticalInput)) : 0f;
        float zRotation = (spaceShipMovementArgs.horizontalInput != 0) ? (this.maxZLocalRotation * Mathf.Sign(spaceShipMovementArgs.horizontalInput)) : 0f;
        this.transform.localRotation = Quaternion.RotateTowards(
            this.transform.localRotation,
            Quaternion.Euler(xRotation, 0, -1 * zRotation),
            this.ZSpeedRotation * Time.deltaTime);
    }
}
