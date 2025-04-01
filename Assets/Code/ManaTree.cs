using UnityEngine;

public class ManaTree : MonoBehaviour
{
    public GameObject crystal;
    public float spawnInterval = 5f;
    public int initialSpawnAmount = 1;
    public int increasePerRound = 1;
    private int currentRound = 0;
    void Start()
    {
        InvokeRepeating("SpawnManaCrystals", 0f, spawnInterval);
    }

    void SpawnManaCrystals()
    {
        int spawnAmount = initialSpawnAmount + (currentRound * increasePerRound);

        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(crystal, transform.position, Quaternion.identity);
        }


        currentRound++;

        void update ()
        {

        }
    }
}
