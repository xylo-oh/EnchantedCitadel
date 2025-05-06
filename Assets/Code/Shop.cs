using UnityEngine;

public class Shop : MonoBehaviour
{
    private int costofDamage = 10; 
    private int costofHealth = 20;
    private int costofSword = 50;

    public int crystalCount = 0;

    void Start()
    {
        
    }

    public void buyDamage()
    {

        if (crystalCount >= costofDamage)
        {
            crystalCount -= costofDamage;
            Debug.Log("Damage purchased!");
        }
        else
        {
            Debug.Log("Not enough crystals to purchase damage.");
        }
    }
    public void buyHealth() {
        if (crystalCount >= costofHealth)
        {
            crystalCount -= costofHealth;
            Debug.Log("Health purchased!");
        }
        else
        {
            Debug.Log("Not enough crystals to purchase health.");
        }
    }

    public void buySword()
    {
        if (crystalCount >= costofSword)
        {
            crystalCount -= costofSword;
            Debug.Log("Sword purchased!");
        }
        else
        {
            Debug.Log("Not enough crystals to purchase sword.");
        }
    }
}
