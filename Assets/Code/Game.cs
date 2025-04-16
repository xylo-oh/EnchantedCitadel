using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public float[] spawnIntervals; 
    public int[] enemiesPerWave; 
    public float spawnDuration = 30f; 

    public int[] enemySpawnWeights;

    private int currentWave = 0;
    private int currentDifficulty = 0; 

    void Start()
    {
        StartCoroutine(SpawnEnemies());

        
        enemySpawnWeights = new int[] { 3, 1, 1 }; 
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
            yield return new WaitForSeconds(2f); 
        }
    }

    private void SpawnWave()
    {
       
        int randomEnemyIndex = WeightedRandom.GetRandomIndex(enemySpawnWeights);
        GameObject enemyToSpawn = enemyPrefabs[randomEnemyIndex];

        
        Instantiate(enemyToSpawn, new Vector3(Random.Range(-5f, 5f), 0, 0), Quaternion.identity);
    }

    public void SetDifficulty(int difficulty)
    {
        currentDifficulty = difficulty;
    }
}


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
