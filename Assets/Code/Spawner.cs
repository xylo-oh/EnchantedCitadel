using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject mutantPrefab; // Prefab of the Mutant enemy
    [SerializeField] private CinemachineDollyCart dollyCart; // Reference to the Cinemachine Dolly Cart
    [SerializeField] private Transform spawnPoint; // Spawn point (start of the path)

    public void SpawnMutant()
    {
        // Instantiate the mutant at the spawn point
        GameObject mutant = Instantiate(mutantPrefab, spawnPoint.position, spawnPoint.rotation);

        // Attach the mutant to the dolly cart
        CinemachineDollyCart mutantCart = mutant.AddComponent<CinemachineDollyCart>();
        mutantCart.m_Path = dollyCart.m_Path; // Assign the same path as the dolly cart
        mutantCart.m_Position = 0f; // Start at the beginning of the path
        mutantCart.m_Speed = dollyCart.m_Speed; // Match the speed of the dolly cart
    }
}

