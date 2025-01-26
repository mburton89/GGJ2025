using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public Transform cameraTransform;


    private void OnTriggerEnter(Collider other)
    {
        // localRotation does what i want it to, so it's what i'm using
        transform.parent.localRotation = Quaternion.Euler(0, transform.parent.localRotation.y, 0);

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
