using UnityEngine;

public class Crystal : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Ensure the crystal has a Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the Crystal prefab!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Optional: Add behavior when the crystal hits the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Stop the crystal from moving after it lands
            rb.isKinematic = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the AddCrystal method on the player
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddCrystal();
            }

            // Destroy the crystal after it is picked up
            Destroy(gameObject);
        }
    }
}

