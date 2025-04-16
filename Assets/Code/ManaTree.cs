using UnityEngine;
public int maxCrystalsPerRound = 10;

void SpawnManaCrystals()
{
    int spawnAmount = Mathf.Min(initialSpawnAmount + (currentRound * increasePerRound), maxCrystalsPerRound);

    for (int i = 0; i < spawnAmount; i++)
    {
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0; 
        spawnPosition += transform.position;

        Instantiate(crystal, spawnPosition, Quaternion.identity);
    }

    currentRound++;
}
public class ManaTree : MonoBehaviour
{
    public GameObject crystal;
    public float spawnInterval = 5f;
    public int initialSpawnAmount = 1;
    public int increasePerRound = 1;
    public float spawnRadius = 2f;
    public int maxCrystalsPerRound = 10; 
    private int currentRound = 0;

    void Start()
    {
        
        InvokeRepeating("SpawnManaCrystals", 0f, spawnInterval);
    }

    void SpawnManaCrystals()
    {
        
        int spawnAmount = Mathf.Min(initialSpawnAmount + (currentRound * increasePerRound), maxCrystalsPerRound);

        for (int i = 0; i < spawnAmount; i++)
        {
            
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 0; 
            spawnPosition += transform.position;

            
            Instantiate(crystal, spawnPosition, Quaternion.identity);
        }

        
        currentRound++;
    }
}
