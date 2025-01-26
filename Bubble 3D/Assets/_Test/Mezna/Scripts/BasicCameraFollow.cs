using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target; // The player or object to follow
    public Vector3 offset = new Vector3(0f, 2f, -7f); // Offset from the target
    public float groundedFollowSpeed = 20f; // How quickly the camera follows the target
    public float aerialFollowSpeed = 45f;
    public float groundedRotationSpeed = 10f; // How smoothly the camera rotates
    public float aerialRotationSpeed = 45f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the target position for the camera
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Smoothly move the camera to the target position
        if (target.GetComponent<MovementController>().IsGrounded())
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, groundedFollowSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, aerialFollowSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }


        // Rotate the camera to match the target's rotation
        Quaternion targetRotation = Quaternion.Euler(0, target.eulerAngles.y, 0); // Match the target's Y-axis rotation
        if (target.GetComponent<MovementController>().IsGrounded())
        {
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, groundedRotationSpeed * Time.deltaTime);
            transform.rotation = smoothedRotation;
        }
        else
        {
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, aerialRotationSpeed * Time.deltaTime);
            transform.rotation = smoothedRotation;
        }
    }
}
