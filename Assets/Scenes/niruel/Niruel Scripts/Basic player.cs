using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basicplayer : MonoBehaviour
{
    [SerializeField]float playerSpeed = 12f;
    [SerializeField] CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * x + transform.forward * z);
        controller.Move(moveDirection * playerSpeed *Time.deltaTime);

        if (transform.position.y< 0 || transform.position.y> 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

    }
}
