using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    public Transform groundCheck;
    private Rigidbody rb;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Add force to accelerate car; go slower if moving backwards
        rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed);
        /*if (Mathf.Sign(Input.GetAxis("Vertical")) == 1)
        {
            rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed);
        }
        else if (Mathf.Sign(Input.GetAxis("Vertical")) == -1)
        {
            rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed / 2);
        }*/

        // Rotate car based on horizontal input and current velocity
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * rb.velocity.magnitude * Time.fixedDeltaTime, 0);

        // Add extra gravity force to car
        rb.velocity -= new Vector3(0, carGravity * Time.deltaTime, 0);

        // Limit movement speed with counter-force; do NOT limit vertical speed or else car gravity gets screwed up
        if (rb.velocity.magnitude > maxMovementSpeed)
        {
            rb.AddForce(-rb.velocity.x, 0, -rb.velocity.z);
        }
    }

    private bool IsGrounded()
    {
        if (groundCheck.GetComponent<GroundCheck>().isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
