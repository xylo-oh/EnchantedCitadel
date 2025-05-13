using UnityEngine;

public class Mutant : MonoBehaviour
{
    private int health = 20;
    private Transform[] waypoints; // Store the assigned waypoints

    void Start()
    {
        // Optional: Start moving towards the first waypoint if needed
    }

    public void AssignWaypoints(Transform[] assignedWaypoints)
    {
        if (assignedWaypoints == null || assignedWaypoints.Length == 0)
        {
            Debug.LogError("No waypoints provided to Mutant!");
            return;
        }

        waypoints = assignedWaypoints;
        Debug.Log($"Waypoints assigned to Mutant: {waypoints.Length} waypoints.");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Weapon hit detected!");

            // Get the Player reference
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            if (player != null)
            {
                int totalDamage = 2 + player.damageModifier; // Base damage + modifier
                TakeDamage(totalDamage);
            }
            else
            {
                Debug.LogError("Player not found!");
            }
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Mutant has died!");
        Destroy(gameObject);
    }
}
