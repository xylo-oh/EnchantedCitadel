using UnityEngine;

public class Mutant : MonoBehaviour
{
    private Transform[] waypoints; // Array of waypoints
    private int currentWaypointIndex = 0; // Index of the current waypoint
    public float speed = 3f; // Movement speed
    private int health = 10; // Enemy starts with 10 health

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if the Mutant has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint
        }

        // Keep the Mutant upright
        KeepUpright();
    }

    public void AssignWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints; // Assign the waypoints dynamically
    }

    private void KeepUpright()
    {
        // Reset rotation to keep the Mutant upright
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
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
        Destroy(gameObject);
    }
}
