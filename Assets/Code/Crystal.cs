using UnityEngine;
using TMPro;

public class Crystal : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("Rigidbody is not attached to the Crystal prefab!");
            enabled = false; // Disable the script to prevent further errors
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the crystal!");

            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.AddCrystal();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Player component not found on the collider or its parent!");
            }
        }
    }
}
