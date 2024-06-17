using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    [Tooltip("Is player controls the spaceship")]
    [SerializeField] private bool isPlayerInSpaceship;

    [SerializeField] private float forwardSpeed = 25f;
    [SerializeField] private float strafeSpeed = 7.5f;
    [SerializeField] private float hoverSpeed = 5f;

    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    [SerializeField] private float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    [SerializeField] private float rollSpeed = 90f;
    [SerializeField] private float rollAcceleration = 3.5f;


    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start()
    {
        this.screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        // For testing only
        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.ExitPlaymode();
        }
        // For testing only
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(this.isPlayerInSpaceship)
        {
            this.lookInput = Input.mousePosition;
            this.mouseDistance.x  = (lookInput.x - this.screenCenter.x) / this.screenCenter.y;
            this.mouseDistance.y  = (lookInput.y - this.screenCenter.y) / this.screenCenter.y;
            this.mouseDistance = Vector2.ClampMagnitude(this.mouseDistance, 1f);

            this.rollInput = Mathf.Lerp(this.rollInput, Input.GetAxisRaw("Roll"), this.rollAcceleration * Time.deltaTime);
            this.transform.Rotate(-this.mouseDistance.y * this.lookRateSpeed * Time.deltaTime,
                this.mouseDistance.x * this.lookRateSpeed * Time.deltaTime,
                this.rollInput * this.rollSpeed * Time.deltaTime,
                Space.Self);

            this.activeForwardSpeed = Mathf.Lerp(this.activeForwardSpeed, Input.GetAxis("Vertical") * this.forwardSpeed, this.forwardAcceleration * Time.deltaTime);
            this.activeStrafeSpeed = Mathf.Lerp(this.activeStrafeSpeed, Input.GetAxis("Horizontal") * this.strafeSpeed, this.strafeAcceleration * Time.deltaTime);
            this.activeHoverSpeed = Mathf.Lerp(this.activeHoverSpeed, Input.GetAxis("Hover") * this.hoverSpeed, this.hoverAcceleration * Time.deltaTime);
            this.transform.position += this.transform.forward * this.activeForwardSpeed * Time.deltaTime;
            this.transform.position += (this.transform.right * this.activeStrafeSpeed * Time.deltaTime) + (this.transform.up * this.activeHoverSpeed * Time.deltaTime);
        }
    }
}
