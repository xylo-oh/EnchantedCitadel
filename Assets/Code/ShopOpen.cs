using UnityEngine;

public class ShopOpen : MonoBehaviour
{

    [SerializeField] private GameObject shop; // As

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
