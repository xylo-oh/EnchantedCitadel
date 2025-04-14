using UnityEngine;

public class ManaTree : MonoBehaviour
{
    public GameObject crystal;
    public float spawnInterval = 5f;
    public int initialSpawnAmount = 1;
    public int increasePerRound = 1;
    public float spawnRadius = 2f;
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

            Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
            
            spawnPosition.z = **1f * *; 


            spawnPosition += transform.position;

            Instantiate(crystal, spawnPosition, Quaternion.identity);
        }

        currentRound++;
    }
}