using UnityEngine;
using System.Collections;
public class WaveSpawner2 : MonoBehaviour
{
    public Transform MutantPrefab;

    public Transform spawnPoint;    

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
          StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;   
        }
        countdown -= Time.deltaTime;
    }
    IEnumerator SpawnWave()
    {
        waveIndex++;

      for (int i = 0; i < waveIndex; i++)
        {
            SpawnMutant();
            yield return new WaitForSeconds(0.5f);
        } 
      
    }
    void SpawnMutant()
    {
        Instantiate(MutantPrefab, spawnPoint.position, spawnPoint.rotation);
    }   
}
