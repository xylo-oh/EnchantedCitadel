using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold your enemy prefabs
    public float[] spawnIntervals; // Array to store spawn intervals for each difficulty
    public int[] enemiesPerWave; // Array to define enemies per wave for each difficulty
    public float spawnDuration = 30f; // Total time to spawn enemies

    public int[] enemySpawnWeights; // Array to define spawn weights for each enemy type

    private int currentWave = 0;
    private int currentDifficulty = 0; // 0: Easy, 1: Medium, 2: Hard

    void Start()
    {
        StartCoroutine(SpawnEnemies());

        // Example spawn weights:
        enemySpawnWeights = new int[] { 3, 1, 1 }; // Weak: 3, Medium: 1, Strong: 1
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentWave < 5)
        {
            for (int i = 0; i < enemiesPerWave[currentDifficulty]; i++)
            {
                SpawnWave();
                yield return new WaitForSeconds(spawnIntervals[currentDifficulty]);
            }
            currentWave++;
            yield return new WaitForSeconds(2f); // Pause between waves
        }
    }

    private void SpawnWave()
    {
        // Choose a random enemy prefab based on weights
        int randomEnemyIndex = WeightedRandom.GetRandomIndex(enemySpawnWeights);
        GameObject enemyToSpawn = enemyPrefabs[randomEnemyIndex];

        // Instantiate the enemy at a random position
        Instantiate(enemyToSpawn, new Vector3(Random.Range(-5f, 5f), 0, 0), Quaternion.identity);
    }

    public void SetDifficulty(int difficulty)
    {
        currentDifficulty = difficulty;
    }
}

// Helper class for weighted random selection
public static class WeightedRandom
{
    public static int GetRandomIndex(int[] weights)
    {
        int totalWeight = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }

        return weights.Length - 1;
    }
}
