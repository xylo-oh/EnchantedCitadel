using UnityEngine;

public class Mutant : MonoBehaviour
{
    private int health = 10; 

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Weapon hit detected!");
            TakeDamage(2); 
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
