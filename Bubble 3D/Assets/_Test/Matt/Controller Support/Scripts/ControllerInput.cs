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

    private Throwing throwing;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        throwing = GetComponent<Throwing>();

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

        // Rotate the player and camera based on the right joystick's horizontal input
        Vector2 rightStickInput = playerInput.actions["Look"].ReadValue<Vector2>();
        float rotation = rightStickInput.x * cameraRotationSpeed * Time.deltaTime;

        // Rotate player
        transform.Rotate(0, rotation, 0);

        // Rotate camera rig if assigned
        if (cameraRig != null)
        {
            cameraRig.Rotate(0, rotation, 0);
        }

        // Jump input
        if (playerInput.actions["Jump"].triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (playerInput.actions["ThrowLeft"].triggered && isGrounded)
        {
            throwing.ThrowLeft();
        }

        if (playerInput.actions["ThrowRight"].triggered && isGrounded)
        {
            throwing.ThrowRight();
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Apply movement (gravity included)
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
