using System;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    
    private void Update()
    {
        SpaceshipIntro(targetObject, .03f);
    }

    private void SpaceshipIntro(Transform referenceObject, float speed)
    {
        transform.position = Vector3.Lerp(transform.position, referenceObject.position,
            speed * Time.deltaTime);
    }
}
