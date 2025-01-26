using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public Transform cameraTransform;


    private void OnTriggerEnter(Collider other)
    {
        // Reset car rotation to be sitting upright upon landing
        // localRotation does what i want it to, so it's what i'm using

        // Rotation of grounded car body (Car)
        transform.parent.parent.localRotation = Quaternion.Euler(0, cameraTransform.localEulerAngles.y, 0);

        // Rotation of aerial car body (Rotation Body)
        transform.parent.localRotation = Quaternion.Euler(0, 0, 0);

        // Check for trick upon landing
        GetComponentInParent<CarTrickDetection>().DetermineTrick();
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
