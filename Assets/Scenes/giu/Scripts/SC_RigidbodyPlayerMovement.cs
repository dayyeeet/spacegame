    using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class SC_RigidbodyPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float speed;
    public float walkSpeed = 10.0f;
    public bool canSprint = true;
    public float sprintSpeed = 25.0f;
    
    [Header("Jumping")]
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    
    [Header("Looking")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }
    
    bool grounded = false;
    Rigidbody r;
    Vector2 rotation = Vector2.zero;
    float maxVelocityChange = 10.0f;
    
    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;
        r.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rotation.y = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        StateHandler();
        
        // Player and Camera rotation
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);

        Quaternion localRotation = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * lookSpeed, 0f);
        transform.rotation = transform.rotation * localRotation;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void StateHandler()
    {
        // Mode - Sprinting
        if (grounded && Input.GetKey("left shift") && canSprint)
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }
        
        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            speed = walkSpeed;  
        } 
        
        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }
    
    private void MovePlayer()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 forwardDir = Vector3.Cross(transform.up, -playerCamera.transform.right).normalized;
            Vector3 rightDir = Vector3.Cross(transform.up, playerCamera.transform.forward).normalized;
            Vector3 targetVelocity = (forwardDir * Input.GetAxis("Vertical") + rightDir * Input.GetAxis("Horizontal")) * speed;

            Vector3 velocity = transform.InverseTransformDirection(r.velocity);
            velocity.y = 0;
            velocity = transform.TransformDirection(velocity);
            Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            velocityChange = transform.TransformDirection(velocityChange);

            r.AddForce(velocityChange, ForceMode.VelocityChange);

            Jump();
        }
        grounded = false;
    }
    void Jump()
    {
        if (Input.GetButton("Jump") && canJump)
        {
            r.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
        }
    }
    
    void OnCollisionStay()
    {
        grounded = true;
    }
}
