using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// thank you deleted reddit user for the tank control script: https://www.reddit.com/r/Unity3D/comments/gd8uwz/3rd_person_tank_control_script/

/* stuff 2 do:
 - make car bounce back when running into walls
 - make car more "weighted" like a car (allow x-rotation to tilt forward and stuff)
*/

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 100;
    public float boostedMovementSpeed = 115;
    public float maxMovementSpeed = 30;
    public float currentSpeed;
    public float rotationSpeed = 3;
    public float jumpForce = 15;
    public float carGravity = 30;

    public float currentAirAmount;
    public float maxAirAmount = 100;

    public float totalXRotation;
    public float totalYRotation;

    private bool isBoosting;

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
    Vector2 keyboardSteerInput;

    public InputActionReference moveStickAction; // Reference to the horizontal input

    public InputActionReference moveUpAction;    // Reference to W input.
    public InputActionReference moveDownAction;  // Reference to S input.
    public InputActionReference moveLeftAction;  // Reference to A input.
    public InputActionReference moveRightAction; // Reference to D input.


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        throwing = GetComponent<Throwing>();

        // Start with half of the air/boost meter filled
        if (FindObjectOfType<UIManager>() != null)
        {
            FindObjectOfType<UIManager>().AdjustSlider(0.5f);
        }
    }

    private void Start()
    {
        GetNewInputActions();
        moveStickAction.action.Enable();
        moveUpAction.action.Enable();
        moveDownAction.action.Enable();
        moveLeftAction.action.Enable();
        moveRightAction.action.Enable();
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}

        if (playerInput.actions["Jump"].triggered && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SoundManager.Instance.PlayJumpSound();
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

        if (playerInput.actions["SteerLeft"].IsPressed())
        {
            //transform.rotation *= Quaternion.Euler(0, -1 * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
            keyboardSteerInput = new Vector2(-1, 0);
        }
        else if (playerInput.actions["SteerRight"].IsPressed())
        {
            //transform.rotation *= Quaternion.Euler(0, 1 * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
            keyboardSteerInput = new Vector2(1, 0);
        }
        else
        {
            keyboardSteerInput = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        currentAirAmount -= 0.1f;
        if (FindObjectOfType<UIManager>() != null)
        {
            FindObjectOfType<UIManager>().AdjustSlider(-0.1f);
        }

        // Rotate car based on horizontal input (and current velocity while grounded)
        if (IsGrounded())
        {
            //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
        }
        else
        {
            //Quaternion lastBodyRotation = rotationBody.localRotation;
            //totalXRotation += Mathf.Abs(rotationBody.localRotation.x - lastBodyRotation.x) * Mathf.Sign(Input.GetAxis("Vertical")) * 180;
            //totalYRotation += Mathf.Abs(rotationBody.localRotation.y - lastBodyRotation.y) * Mathf.Sign(Input.GetAxis("Horizontal")) * 180;

            Quaternion lastBodyRotation = rotationBody.localRotation;


            Vector2 stickInput = moveStickAction.action.ReadValue<Vector2>();

            // Extract vertical (Y) and horizontal (X) inputs.
            float verticalInput = stickInput.y;
            float horizontalInput = stickInput.x;

            if (moveUpAction.action.IsPressed()) verticalInput += 1f;
            if (moveDownAction.action.IsPressed()) verticalInput -= 1f;

            if (moveLeftAction.action.IsPressed()) horizontalInput -= 1f;
            if (moveRightAction.action.IsPressed()) horizontalInput += 1f;

            // Calculate cumulative rotations.
            totalXRotation += Mathf.Abs(rotationBody.localRotation.eulerAngles.x - lastBodyRotation.x) * Mathf.Sign(verticalInput) * 180;
            totalYRotation += Mathf.Abs(rotationBody.localRotation.eulerAngles.y - lastBodyRotation.y) * Mathf.Sign(horizontalInput) * 180;

            // Update the last rotation for the next frame.
        }

        // Add extra gravity force to car
        rb.velocity -= new Vector3(0, carGravity * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else
        {
            isBoosting = false;
        }

        // Limit movement speed with counter-force; do NOT limit vertical speed or else car gravity gets screwed up
        if ((rb.velocity.magnitude > maxMovementSpeed && !isBoosting) || (rb.velocity.magnitude > boostedMovementSpeed && isBoosting))
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
        float horizontalInput = steerInput.x + keyboardSteerInput.x;

        // Apply rotation based on the horizontal input
        if (IsGrounded())
        {
            transform.rotation *= Quaternion.Euler(0, horizontalInput * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);
        }
        else
        {
            rotationBody.rotation *= Quaternion.Euler(steerInput.y * rotationSpeed * 60 * Time.fixedDeltaTime, horizontalInput * rotationSpeed * 60 * Time.fixedDeltaTime, 0);
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

        if (forwardForce > 0.1f)
        {
            if (!SoundManager.Instance.isPlayingMotorSound)
            {
                SoundManager.Instance.StartMotorSound();
            }
        }
        else
        {
            SoundManager.Instance.StopMotorSound();
        }
    }
}
