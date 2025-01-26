using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;
    public Transform cameraTransform;


    private void OnTriggerEnter(Collider other)
    {
        transform.parent.localRotation = Quaternion.Euler(0, cameraTransform.rotation.y, 0);
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
