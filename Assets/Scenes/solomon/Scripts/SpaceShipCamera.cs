using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SpaceShipCamera : MonoBehaviour
{
    private Vector3 initialLocalPosition;
    private Vector3 finalLocalPosition;
    [Range(2f, 40f)]
    [SerializeField] private float maxCameraBackDistance = 5f;
    [Range(1f, 20f)]
    [SerializeField] private float cameraMovingSpeed = 5f;

    void OnEnable()
    {
        SpaceShipMovementEvent spaceshipMovementEvent = this.transform.parent.GetComponent<SpaceShipMovementEvent>();
        if (spaceshipMovementEvent is null)
        {
            Debug.LogError("Parent doesn't have SpaceShipMovementEvent class");
            return;
        }
        spaceshipMovementEvent.OnSpaceshipMovement += OnSpaceShipMovementEvent_MoveCamera;
    }

    void Start()
    {
        this.initialLocalPosition = this.transform.localPosition;
        this.finalLocalPosition = this.initialLocalPosition + Vector3.back * maxCameraBackDistance;
    }

    private void OnSpaceShipMovementEvent_MoveCamera(SpaceShipMovementEvent spaceShipMovementEvent, SpaceShipMovementArgs spaceShipMovementArgs)
    {
        if (spaceShipMovementArgs.verticalInput > 0.8)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.finalLocalPosition, this.cameraMovingSpeed * Time.deltaTime);
        }
        else if(spaceShipMovementArgs.verticalInput <= 0)
        {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.initialLocalPosition, this.cameraMovingSpeed * Time.deltaTime);
        }
    }
}
