using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// thank you deleted reddit user for the tank control script: https://www.reddit.com/r/Unity3D/comments/gd8uwz/3rd_person_tank_control_script/

/* stuff 2 do:
 - make car bounce back when running into walls
 - make car more "weighted" like a car (allow x-rotation to tilt forward and stuff)
 - make car rotate freely instead of accelerating while in the air
*/

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 100;
    public float maxMovementSpeed = 30;
    public float rotationSpeed = 3;
    public float jumpForce = 15;
    public float carGravity = 30;

    private Vector3 movementVelocity = Vector3.zero;
    private Vector3 lastVelocity;

    public Transform groundCheck;
    public Transform rotationBody;
    private Rigidbody rb;
    public Transform cameraTransform;

    private PlayerInput playerInput;
    private Throwing throwing;

    private InputAction steerAction;

    public float controllerAccelerationSpeed = 10f;
    public float controllerReverseSpeed = 5f;

    private InputAction controllerAccelerateAction;
    private InputAction controllerReverseAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        throwing = GetComponent<Throwing>();
    }

    private void Start()
    {
        GetNewInputActions();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (playerInput.actions["Jump"].triggered && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (playerInput.actions["ThrowLeft"].triggered)
        {
            throwing.ThrowLeft();
        }

        if (playerInput.actions["ThrowRight"].triggered)
        {
            throwing.ThrowRight();
        }

        if (playerInput.actions["ThrowForward"].triggered)
        {
            throwing.ThrowForward();
        }

    }

    private void FixedUpdate()
    {
        // Rotate car based on horizontal input (and current velocity while grounded)
        if (IsGrounded())
        {
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
        }
        else
        {
            rotationBody.transform.rotation *= Quaternion.Euler(Input.GetAxis("Vertical") * rotationSpeed * 60 * Time.fixedDeltaTime, Input.GetAxis("Horizontal") * rotationSpeed * 60 * Time.fixedDeltaTime, 0);
        }

        // Add extra gravity force to car
        rb.velocity -= new Vector3(0, carGravity * Time.deltaTime, 0);

        // Limit movement speed with counter-force; do NOT limit vertical speed or else car gravity gets screwed up
        if (rb.velocity.magnitude > maxMovementSpeed)
        {
            rb.AddForce(-rb.velocity.x, 0, -rb.velocity.z);
        }

        HandleNewInput();
    }

    public bool IsGrounded()
    {
        if (groundCheck.GetComponent<GroundCheck>().isGrounded)
        {
            return true;
        }
        else
        {
            lastVelocity = rb.velocity;
            return false;
        }
    }

    void GetNewInputActions()
    {
        steerAction = playerInput.actions["Steer"];
        controllerAccelerateAction = playerInput.actions["Accelerate"];
        controllerReverseAction = playerInput.actions["Reverse"];
    }

    void HandleNewInput()
    {
        // Read the Vector2 value from the Steer action
        Vector2 steerInput = steerAction.ReadValue<Vector2>();

        // Extract the horizontal component for steering
        float horizontalInput = steerInput.x;

        // Apply rotation based on the horizontal input
        if (IsGrounded())
        {
            transform.rotation *= Quaternion.Euler(0, horizontalInput * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
        }
        else
        {
            rotationBody.transform.rotation *= Quaternion.Euler(steerInput.y * rotationSpeed * 60 * Time.fixedDeltaTime, horizontalInput * rotationSpeed * 60 * Time.fixedDeltaTime, 0);
        }

        float accelerateInput = controllerAccelerateAction.ReadValue<float>();
        //print("accelerateInput " + accelerateInput);
        float reverseInput = controllerReverseAction.ReadValue<float>();

        // Calculate forward and backward forces
        float forwardForce = accelerateInput * controllerAccelerationSpeed;
        float backwardForce = reverseInput * controllerReverseSpeed;

        // Apply forces to the Rigidbody
        if (IsGrounded())
        {
            rb.AddForce(transform.forward * (forwardForce - backwardForce) * maxMovementSpeed, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity = lastVelocity;
        }
    }
}
