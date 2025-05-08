//#define USE_CROUCH
#define MOUSE_SMOOTHING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 3.0f;

    private CharacterController controller;
    private Vector3 move_input;
    private Vector3 move_direction;
    private Vector3 move_vector;
    private Vector3 vector_down;
    private Quaternion player_rotation;
    private float look_y = 0.0f;
    public GameObject Sword;

    public int crystalCount = 0; // Track the number of crystals collected
    public TMP_Text crystalCountText; // Use TMP_Text for TextMeshPro
    public int damageModifier = 0; // Tracks the additional damage from upgrades


    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController is not attached to the Player GameObject!");
        }

        vector_down = Vector3.down; // Initialize vector_down to point downward
        player_rotation = transform.rotation; // Initialize player_rotation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateCrystalUI(); // Initialize the UI with the current crystal count
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleCamera();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ATTACK());
        }
    }

    IEnumerator ATTACK()
    {
        Sword.GetComponent<Animator>().Play("Attack");
        yield return new WaitForSeconds(0.5f);
        Sword.GetComponent<Animator>().Play("Idle");
    }

    private void HandleInput()
    {
        // Get movement input
        move_input.x = Input.GetAxisRaw("Horizontal");
        move_input.z = Input.GetAxisRaw("Vertical");

        // Get mouse input for rotation
        float rotation_input = Input.GetAxis("Mouse X") * rotationSpeed;
        float look_input = Input.GetAxis("Mouse Y") * rotationSpeed * 0.9f; // Make vertical mouse look less sensitive

        // Update player rotation
        player_rotation *= Quaternion.Euler(0, rotation_input, 0);

        // Update camera vertical rotation
        look_y -= look_input;
        look_y = Mathf.Clamp(look_y, -90.0f, 90.0f);
    }

    private void HandleMovement()
    {
        // Apply player rotation to movement direction
        move_direction = player_rotation * move_input.normalized;

        // Apply gravity and handle jumping
        if (controller.isGrounded)
        {
            move_vector.y = -1f; // Small downward force to keep grounded

            // Check for jump input
            if (Input.GetButtonDown("Jump"))
            {
                move_vector.y = 5.8f; // Apply jump velocity (adjust as needed)
            }
        }
        else
        {
            // Apply gravity when in the air
            move_vector.y += Physics.gravity.y * Time.deltaTime;
        }

        // Combine movement direction with gravity
        move_vector.x = move_direction.x * moveSpeed;
        move_vector.z = move_direction.z * moveSpeed;

        // Move the player
        controller.Move(move_vector * Time.deltaTime);
    }

    private void HandleCamera()
    {
        // Apply horizontal rotation to the player
        transform.rotation = player_rotation;

        // Apply vertical rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(look_y, 0, 0);
    }

    public void AddCrystal()
    {
        crystalCount++;
        Debug.Log("Crystals Collected: " + crystalCount);
        UpdateCrystalUI(); // Update the UI when a crystal is collected
    }

  public void UpdateCrystalUI()
    {
        if (crystalCountText != null)
        {
            crystalCountText.text = "Crystals: " + crystalCount; // Update the UI text
        }
    }
}

    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float moveSpeed = 5.0f;
        public float rotationSpeed = 3.0f;
        public float friction = 5.0f;
        public float deccSpeed = 3.5f;
        public float accSpeed = 10.0f;
        public float accSpeedAir = 1.5f;
        public float jumpVelocity = 5.8f;
        public float jumpAcceleration = 1.42f;
        public float gravity = 17.0f;
        public float cameraOffset = 0.72f;
        public float playerHeight = 1.8f;
    }
