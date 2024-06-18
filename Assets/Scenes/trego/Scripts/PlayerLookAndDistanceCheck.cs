using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLookAndDistanceCheck : MonoBehaviour
{
    public Camera playerCamera;
    public float maxInteractionDistance = 3f; // Maximum distance to interact with objects

    [SerializeField] private ItemConverterOnPlayer itemConverterOnPlayer;

    private Outline _outline;

    private void Start()
    {
        itemConverterOnPlayer = GetComponent<ItemConverterOnPlayer>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxInteractionDistance))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                // Player is looking directly at the object and within interaction distance
                _outline = hit.collider.gameObject.GetComponent<Outline>();
                
                // Make outline visible
                if (_outline.enabled == false)
                {
                    _outline.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.collider.gameObject);
                    itemConverterOnPlayer.GetFuel(5f);
                }
            }
        }
        else
        {
            //// If no object is hit by the ray or it's too far away, disable the outline
            //if (_outline.enabled == true)
            //{
            //    _outline.enabled = false;
            //}
        }
    }
}