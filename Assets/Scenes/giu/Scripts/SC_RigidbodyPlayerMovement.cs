    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.Serialization;

    [RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class SC_RigidbodyPlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    private float speed;
    public float walkSpeed = 3.0f;
    
    [Header("Sprinting")]
    public bool canSprint = true;
    public float sprintSpeed = 7.0f;
    public float sprintTime = 5.0f;
    public float staminaReplenishFactor = 0.25f;
    
    [Header("Crouching")]
    public float crouchSpeed = 1.0f;
    public float crouchYScale = 0.5f;
    private float startYScale; 
    
    [Header("Jumping")]
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    
    [Header("Looking")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    [Header("HP")] 
    private float hp = 100.0f;
    private float vyCache;
    public float hpImpactThreshold = 2000.0f;
    public float hpSmallBruises = 3000.0f;
    public float hpNormalInjuries = 5000.0f;
    public float hpSeriousInjuries = 8000.0f;
    public float hpFatalInjuries = 10000.0f;
    public float hpDeath = 15000.0f;

    private MovementState state;
    private enum MovementState
    {
        walking,
        sprinting,
        crouching,
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

        startYScale = transform.localScale.y;
    }

    void Update()
    {
        Crouch();
        // Player and Camera rotation
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);

        Quaternion localRotation = Quaternion.Euler(0f, Input.GetAxis("Mouse X") * lookSpeed, 0f);
        transform.rotation = transform.rotation * localRotation;
       
        StateHandler();

        }

    void FixedUpdate()
    {
        SprintTime();
        MovePlayer();
        vyCache = r.velocity.y;
       }
    
    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetButton("Control"))
        {
            state = MovementState.crouching;
            speed = crouchSpeed;
        }
        
        // Mode - Sprinting
        if (grounded && Input.GetButton("Shift") && canSprint && sprintTime > 0)
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }
        
        // Mode - Walking
        else if (grounded && !Input.GetButton("Control"))
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

    void Crouch()
    {
        // start crouch
        if (Input.GetButtonDown("Control"))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            r.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetButtonUp("Control"))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    void SprintTime()
    {
        if (state == MovementState.sprinting)
        {
            if (sprintTime > 0)
            {
                sprintTime -= Time.deltaTime;
            }
        }

        if (state != MovementState.sprinting && sprintTime < 5.0f)
        {
            sprintTime += Time.deltaTime * staminaReplenishFactor;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (Vector3.Dot(col.GetContact(0).normal, Vector3.up) < 0.5f)
        {
            if (r.velocity.y < -0.5f)
            {
                r.velocity = Vector3.up * r.velocity.y;
                return;
            }
        }

        float acceleration = (r.velocity.y - vyCache) / Time.fixedDeltaTime;
        float impactForce = r.mass * Mathf.Abs(acceleration);

        float hpDamage = CalculateDamage(impactForce);
        
        hp -= hpDamage;
        Debug.Log("Der Spieler hat Schaden erhalten: " + hpDamage + " ImpactForce war " + impactForce);
    }
    
    float CalculateDamage(float impactForce)
    {
        if (impactForce < hpImpactThreshold)
            return 0; // No damage
        else if (impactForce < hpSmallBruises)
            return Mathf.Round((float)Mathf.Lerp(1, 10,
                (impactForce - hpImpactThreshold) / (hpSmallBruises - hpImpactThreshold)) * 10.0f) * 0.1f;
        else if (impactForce < hpNormalInjuries)
            return Mathf.Round((float)Mathf.Lerp(10, 20,
                (impactForce - hpSmallBruises) / (hpNormalInjuries - hpSmallBruises)) * 10.0f) * 0.1f;
        else if (impactForce < hpSeriousInjuries)
            return Mathf.Round((float)Mathf.Lerp(20, 50,
                (impactForce - hpNormalInjuries) / (hpSeriousInjuries - hpNormalInjuries)) * 10.0f) * 0.1f;
        else if (impactForce < hpFatalInjuries)
            return Mathf.Round((float)Mathf.Lerp(50, 80,
                (impactForce - hpSeriousInjuries) / (hpFatalInjuries - hpSeriousInjuries)) * 10.0f) * 0.1f;
        else if (impactForce < hpDeath)
            return Mathf.Round((float)Mathf.Lerp(80, 100,
                (impactForce - hpFatalInjuries) / (hpDeath - hpFatalInjuries)) * 10.0f) * 0.1f;
        else
            return 100; // Death
    }
    void OnCollisionStay()
    {
        grounded = true;
    }
}
