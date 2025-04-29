using UnityEngine;

public class Mutant : MonoBehaviour
{
    private int health = 10; // Enemy starts with 10 health

    void Start()
    {
        Debug.Log("Mutant spawned with health: " + health);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Weapon"
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(2); // Sword does 2 damage
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage; // Reduce health by the damage amount
        Debug.Log("Mutant took damage! Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Mutant has died!");
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
