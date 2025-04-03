using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 2f;
    public float manaPickupRadius = 2f;

    public Transform playerCamera;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private bool isGrounded = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = playerCamera.transform.TransformDirection(movement);
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            isGrounded = true;
        }

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
