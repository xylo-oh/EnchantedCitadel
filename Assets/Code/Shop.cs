using UnityEngine;

public class Shop : MonoBehaviour
{
    private int costofDamage = 10;
    private int costofHealth = 20;
    private int costofSword = 50;

    private GameObject shopPanel;
    public Player player;
    private Health health;


    void Start()
    {
        shopPanel = GameObject.Find("ShopPanel");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (shopPanel.activeSelf)
        {
            Debug.Log("Press 1, 2, or 3 to purchase.");
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
    }

    public void buyDamage()
    {
        if (player.crystalCount >= costofDamage)
        {
            player.crystalCount -= costofDamage;
            player.damageModifier += 1; // Increase the damage modifier
            player.UpdateCrystalUI(); // Update the UI after purchase
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

            // Find the Health component and increase health
            Health health = health.GetComponent<Health>();
            if (health != null)
            {
                health.IncreaseHealth(25); // Increase health by 25
            }
            else
            {
                Debug.LogError("Health component not found on the Player!");
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
            Debug.Log("Sword purchased!");
        }
        else
        {
            Debug.Log("Not enough crystals to purchase sword.");
        }
    }
}
