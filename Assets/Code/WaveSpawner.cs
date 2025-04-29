using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [SerializeField] private GameObject spawnpoint;

    public Wave[] waves;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Update()
    {
        if (currentWaveIndex >= waves.Length)
        {
            // All waves completed
            return;
        }

        countdown -= Time.deltaTime;

        if (countdown <= 0 && !isSpawning)
        {
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;

        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.enemies.Length; i++)
        {
            Mutant enemy = Instantiate(currentWave.enemies[i], spawnpoint.transform.position, Quaternion.identity);
            enemy.transform.SetParent(spawnpoint.transform);

            yield return new WaitForSeconds(currentWave.timeToNextEnemy);
        }

        isSpawning = false;

        // Move to the next wave
        currentWaveIndex++;
    }
}

[System.Serializable]
public class Wave
{
    public Mutant[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}