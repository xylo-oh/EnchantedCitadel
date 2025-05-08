using UnityEngine;

public class Shop : MonoBehaviour
{
    private int costofDamage = 10;
    private int costofHealth = 20;
    private int costofSword = 50;

    private GameObject shopPanel;
    public Player player;
    private Health health;
    public GameObject newSwordPrefab;


    void Start()
    {
        shopPanel = GameObject.Find("ShopPanel");
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Find the GameObject with the Health script
        GameObject healthObject = GameObject.FindWithTag("HealthObject"); // Replace "HealthObject" with the actual tag or name
        if (healthObject != null)
        {
            health = healthObject.GetComponent<Health>();
        }
        else
        {
            Debug.LogError("Health object not found! Ensure the GameObject is tagged correctly.");
        }
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
            Debug.Log("Sword purchased!");
        }
        else
        {
            Debug.Log("Not enough crystals to purchase sword.");
        }
    }
}
