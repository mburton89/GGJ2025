using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// thank you deleted reddit user for the tank control script: https://www.reddit.com/r/Unity3D/comments/gd8uwz/3rd_person_tank_control_script/

/* stuff 2 do:
 - make car bounce back when running into walls
 - make car more "weighted" like a car (allow x-rotation to tilt forward and stuff)
 - make car rotation behave more like the car has wheels rather than just spinning the whole car around
*/

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 15;
    public float maxMovementSpeed = 15;
    public float rotationSpeed = 90;
    public float jumpForce = 8;
    public float carGravity = 15;
    //public float cameraDistance = 7;
    //public float cameraHeight = 2;

    private Vector3 movementVelocity = Vector3.zero;

    public Transform groundCheck;
    private Rigidbody rb;
    private Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
        if (Mathf.Sign(Input.GetAxis("Vertical")) == 1)
        {
            rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed);
        }
        else
        {
            rb.AddForce(transform.rotation * new Vector3(0, 0, Input.GetAxis("Vertical")) * movementSpeed / 2);
        }

        // Rotate car based on horizontal input
        // TO DO: update car rotation to not rotate whole car, but just "wheels" + add limit to how far they can rotate
        // May be able to use same code here, just add rotation limit and don't rotate car so camera doesn't follow and give the wrong idea
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime, 0);

        // Add extra gravity force
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

    // DEBUG
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), transform.position.z), Vector3.down);
    }
}
