using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    private int costofDamage = 10;
    private int costofHealth = 20;
    private int costofSword = 100;

    [SerializeField] private GameObject shopPanel;
    public Player player;
    private Health health;
    private bool hasShownShopMessage = false;
    [SerializeField] private GameObject gameWinPanel; // Reference to the Game Over UI panel
    public UnityEvent onGameWin;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        GameObject healthObject = GameObject.FindWithTag("HealthObject"); 
        if (healthObject != null)
        {
            health = healthObject.GetComponent<Health>();
        }
        else
        {
            Debug.LogError("Health object not found! Ensure the GameObject is tagged correctly.");
        }
        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(false); // Ensure the Game Over panel is hidden at the start
        }
    }

    void Update()
    {
        if (shopPanel != null && shopPanel.activeSelf)
        {
            if (!hasShownShopMessage)
            {
                Debug.Log("Press 1, 2, or 3 to purchase.");
                hasShownShopMessage = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buyDamage();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                buyHealth();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                buySword();
            }
        }
        else
        {
            hasShownShopMessage = false; // Reset when shop closes
        }
    }

    public void buyDamage()
    {
        if (player.crystalCount >= costofDamage)
        {
            player.crystalCount -= costofDamage;
            player.damageModifier += 1;
            player.UpdateCrystalUI();
            Debug.Log("Damage purchased! Damage modifier is now: " + player.damageModifier);
        }
        else
        {
            Debug.Log("Not enough crystals to purchase damage.");
        }
    }
   
        

public void buyHealth()
{
    if (player.crystalCount >= costofHealth)
    {
        player.crystalCount -= costofHealth;

        // Increase health using the Health script
        if (health != null)
        {
            health.IncreaseHealth(25); // Increase health by 25
        }
        else
        {
            Debug.LogError("Health component not found!");
        }

        player.UpdateCrystalUI(); // Update the UI after purchase
        Debug.Log("Health purchased!");
    }
    else
    {
        Debug.Log("Not enough crystals to purchase health.");
    }
}

    public void buySword()
    {
        if (player.crystalCount >= costofSword)
        {
            player.crystalCount -= costofSword;
            player.UpdateCrystalUI(); // Update the UI after purchase
            GameWin();
        }
        else
        {
            Debug.Log("Not enough crystals to win.");
        }
    }
    void GameWin()
    {
        Debug.Log("You Won!"); // Log game over for debugging

        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(true);
            shopPanel.SetActive(false);
        }

        Time.timeScale = 0; 
        onGameWin?.Invoke();
    }
}
