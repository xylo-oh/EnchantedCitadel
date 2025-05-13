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

            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.AddCrystal();
                Destroy(gameObject);
            }
        }
    }
}
