using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ControllerInput : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Transform cameraRig; // The camera rig to rotate
    public float cameraRotationSpeed = 100f;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private PlayerInput playerInput;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        // Enable input actions
        playerInput.actions.Enable();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Get movement input
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        // Apply movement relative to the player's facing direction
        characterController.Move(transform.TransformDirection(move) * moveSpeed * Time.deltaTime);

        // Rotate the camera rig based on horizontal input
        if (cameraRig != null)
        {
            float horizontalInput = input.x; // Get horizontal input from the joystick
            cameraRig.Rotate(0, horizontalInput * cameraRotationSpeed * Time.deltaTime, 0);
        }

        // Jump input
        if (playerInput.actions["Jump"].triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Apply movement (gravity included)
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
