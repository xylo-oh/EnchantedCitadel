using UnityEngine;

public class ShopOpen : MonoBehaviour
{

    private GameObject shop; 

    void Start()
    {
        shop = GameObject.Find("ShopPanel");
        shop.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            shop.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            shop.SetActive(false);
        }
    }
}
