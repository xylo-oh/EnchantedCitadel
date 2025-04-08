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
        
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);
        playerBody.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = playerCamera.transform.TransformDirection(movement);
        movement.Normalize(); 

        
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z);
            isGrounded = false;
        }

        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void FixedUpdate()
    {
        
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
