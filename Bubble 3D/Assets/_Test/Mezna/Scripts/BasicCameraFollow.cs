using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target; // The player or object to follow
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Offset from the target
    public float followSpeed = 10f; // How quickly the camera follows the target
    public float rotationSpeed = 5f; // How smoothly the camera rotates

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the target position for the camera
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Smoothly move the camera to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Rotate the camera to match the target's rotation
        Quaternion targetRotation = Quaternion.Euler(0, target.eulerAngles.y, 0); // Match the target's Y-axis rotation
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the rotation
        transform.rotation = smoothedRotation;
    }
}
