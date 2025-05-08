using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject mutantPrefab; // The Mutant prefab
    public Transform[] waypoints; // The waypoints in the scene

    public void SpawnMutant()
    {
        Debug.Log("SpawnMutant method called!"); // Debug log to confirm the method is triggered

        // Instantiate the Mutant prefab at the position of the first waypoint
        GameObject mutantInstance = Instantiate(mutantPrefab, waypoints[0].position, Quaternion.identity);

        if (mutantInstance != null)
        {
            Debug.Log("Mutant prefab instantiated successfully!");
        }
        else
        {
            Debug.LogError("Failed to instantiate Mutant prefab!");
        }

        // Assign the waypoints to the Mutant
        Mutant mutantScript = mutantInstance.GetComponent<Mutant>();
        if (mutantScript != null)
        {
            mutantScript.AssignWaypoints(waypoints);
            Debug.Log("Waypoints assigned to Mutant.");
        }
        else
        {
            Debug.LogError("Mutant script is missing on the instantiated prefab!");
        }
    }

    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is empty in WaveSpawner!");
        }
        else
        {
            Debug.Log($"Waypoints assigned: {waypoints.Length} waypoints.");
        }

        // Test spawning a Mutant
        SpawnMutant();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMutant();
        }
    }
}