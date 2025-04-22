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
            // Generate a random position within a circle
            Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(spawnRadius * 0.5f, spawnRadius);
            Vector3 spawnPosition = new Vector3(randomCircle.x, 0.5f, randomCircle.y);

            // Offset the position relative to the tree's position
            spawnPosition += transform.position;

            // Instantiate the crystal
            Instantiate(crystal, spawnPosition, Quaternion.identity);
        }

        currentRound++;
    }
}