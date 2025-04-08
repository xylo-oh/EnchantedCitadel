using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 2f;
    public float manaPickupRadius = 2f;

    public Transform playerCamera;
    public Transform playerBody;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private bool isGrounded = true;
    private Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        playerCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        // Mouse input for camera rotation
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Apply rotations
        playerCamera.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
        playerBody.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = playerCamera.transform.TransformDirection(movement);

        // Move using Rigidbody
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        // Ground detection
        isGrounded = IsGrounded();
    }

    bool IsGrounded()
    {
        // Raycast down from the center of the player
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void FixedUpdate()
    {
        // Mana crystal pickup
        Collider[] nearbyCrystals = Physics.OverlapSphere(transform.position, manaPickupRadius);

        foreach (Collider crystalCollider in nearbyCrystals)
        {
            if (crystalCollider.CompareTag("ManaCrystal"))
            {
                Destroy(crystalCollider.gameObject);
            }
        }
    }
}