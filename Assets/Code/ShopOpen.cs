using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    public GameObject shopPanel; 

    void Start()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }
}
