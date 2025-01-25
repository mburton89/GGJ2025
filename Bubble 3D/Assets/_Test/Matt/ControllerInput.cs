using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ControllerInput : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Input")]
    public InputActionReference moveAction; // Reference to movement action
    public InputActionReference jumpAction; // Reference to jump action

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Enable Input Actions
        moveAction.action.Enable();
        jumpAction.action.Enable();
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
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        // Apply movement relative to the player's facing direction
        characterController.Move(transform.TransformDirection(move) * moveSpeed * Time.deltaTime);

        // Jump input
        if (jumpAction.action.triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Apply movement (gravity included)
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void OnDisable()
    {
        // Disable Input Actions when the script is disabled
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }
}
