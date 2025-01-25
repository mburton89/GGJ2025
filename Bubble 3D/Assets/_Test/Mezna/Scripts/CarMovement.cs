using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// thank you deleted reddit user for the tank control script: https://www.reddit.com/r/Unity3D/comments/gd8uwz/3rd_person_tank_control_script/

/* stuff 2 do:
 - add more friction/higher deceleration to car
 - make camera more reactive to movement (higher fov, lag behind player rotation slightly)
 - fix jumping
*/

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 15;
    public float maxMovementSpeed = 15;
    public float rotationSpeed = 90;
    public float jumpForce = 8;
    public float cameraDistance = 7;
    public float cameraHeight = 2;
    public bool isGrounded;

    private Vector3 movementVelocity = Vector3.zero;

    public Transform groundCheck;
    private Rigidbody rb;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    private void FixedUpdate()
    {
        //transform.position += transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed * Time.fixedDeltaTime;
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime, 0);

        rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed);
        //rb.AddTorque(Input.GetAxis("Vertical") * rotationSpeed * transform.up);

        if (rb.velocity.magnitude > maxMovementSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMovementSpeed;
        }

        cameraTransform.position = transform.position + transform.rotation * new Vector3(0, cameraHeight, -cameraDistance);
        cameraTransform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Simple way to check for ground
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
