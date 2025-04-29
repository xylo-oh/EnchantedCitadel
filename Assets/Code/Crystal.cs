using UnityEngine;

public class Crystal : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the Crystal prefab!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the crystal!"); // Debugging line

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddCrystal();
            }

            Destroy(gameObject);
        }
    }
}

