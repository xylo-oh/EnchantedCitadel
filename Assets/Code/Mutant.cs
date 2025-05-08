using UnityEngine;

public class Mutant : MonoBehaviour
{
    public float speed = 10f;
    private int wavepointIndex = 0;
    private Transform target; 

    private int health = 10;

    void Start()
    {
        // Ensure the Rigidbody exists and freeze rotation
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Initialize the target
        target = WayPoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }

        void GetNextWayPoint()
        {

            if(wavepointIndex >= WayPoints.points.Length - 1)
            {
                Destroy(gameObject);
                return;
            }
            
            wavepointIndex++;
            target = WayPoints.points[wavepointIndex];
        }
           
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
