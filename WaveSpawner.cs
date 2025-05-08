using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int count;          // Number of mutants in this wave
        public float spawnRate;    // Time between spawns
    }

    public List<Wave> waves;       // List of waves
    public Transform spawnPoint;   // Spawn location for mutants
    public GameObject mutantPrefab; // Prefab for the mutant
    public float timeBetweenWaves = 5f; // Time between waves

    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];
            yield return StartCoroutine(SpawnWave(currentWave));
            currentWaveIndex++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log($"Spawning wave {currentWaveIndex + 1}");
        for (int i = 0; i < wave.count; i++)
        {
            SpawnMutant();
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
    }

    void SpawnMutant()
    {
        if (mutantPrefab != null)
        {
            GameObject mutant = Instantiate(mutantPrefab, spawnPoint.position, Quaternion.identity);
            mutant.tag = "Enemy"; // Ensure the spawned mutant is tagged as "Enemy"
        }
        else
        {
            Debug.LogWarning("Mutant prefab is not assigned!");
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}