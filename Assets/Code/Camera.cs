using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float mouseSensitivity = 2f;
    public float cameraDistance = 2f;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        // Mouse Look (Control Camera Rotation)
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);

        // Camera Movement
        transform.position = playerTransform.position + playerTransform.forward * cameraDistance;
    }
}