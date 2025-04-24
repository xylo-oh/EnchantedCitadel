using TMPro;
using UnityEngine.Events;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.Events; // Required for UnityEvent
using TMPro; // Required for TextMeshPro

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health of the castle
    private int currentHealth;

    [SerializeField] private TMP_Text healthText; // Use TMP_Text for TextMeshPro
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over UI panel

    [SerializeField] private int damagePerHit = 5; // Damage taken per enemy collision

    public UnityEvent onGameOver; // Event triggered on game over

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        UpdateHealthUI(); // Update the health display

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Ensure the Game Over panel is hidden at the start
        }
        else
        {
            Debug.LogWarning("GameOverPanel is not assigned in the inspector.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}"); // Log the collision

        // Check if the object colliding with the castle is tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collided with the castle!"); // Log enemy collision
            TakeDamage(damagePerHit); // Reduce health by the configured damage

            // Destroy the enemy object
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        if (damage <= 0) return; // Ignore invalid damage values

        currentHealth -= damage; // Reduce health
        currentHealth = Mathf.Max(currentHealth, 0); // Clamp health to a minimum of 0
        UpdateHealthUI(); // Update the health display

        if (currentHealth <= 0)
        {
            GameOver(); // Trigger game over if health drops to zero
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}"; // Update the health text
        }
        else
        {
            Debug.LogWarning("HealthText is not assigned in the inspector.");
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // Log game over for debugging

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Show the Game Over panel
        }

        Time.timeScale = 0; // Pause the game

        // Trigger the game over event
        onGameOver?.Invoke();
    }

    // Optional: Public method to reset health (useful for restarting the game)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1; // Resume the game
    }
}
