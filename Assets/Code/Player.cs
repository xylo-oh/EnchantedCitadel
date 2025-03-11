using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 2f;
    public float manaPickupRadius = 2f;

    // Camera
    public Transform playerCamera; // Reference to your camera object

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private bool isGrounded = true;

    void Start()
    {
        // Ensure the camera is initially at the correct rotation
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse to the game window
        playerCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // Reset camera rotation
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = transform.TransformDirection(movement);
        transform.position += movement * speed * Time.deltaTime;

        // Mouse Look (Control Camera Rotation)
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Apply rotation to the camera, NOT the player
        playerCamera.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        // Ground Check
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            isGrounded = true;
        }

        // Mana Crystal Pickup (Same as before)
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