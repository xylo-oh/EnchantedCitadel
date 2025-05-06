using UnityEngine;
using System.Collections; // Required for IEnumerator
using UnityEngine.Splines; // Required for SplineContainer

public class Spawner : MonoBehaviour
{
    public GameObject mutantPrefab; // Prefab of the mutant to spawn
    public SplineContainer splineContainer; // Reference to the Spline Container
    public float spawnInterval = 5f; // Time interval between spawns
    public int initialSpawnCount = 3; // Number of mutants to spawn at the start
    public float mutantSpeed = 2f; // Speed of the mutants along the spline

    private IEnumerator Start()
    {
        // Spawn initial mutants with a small delay
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnMutant();
            yield return new WaitForSeconds(0.5f); // Small delay between spawns
        }

        // Start spawning mutants at intervals
        InvokeRepeating(nameof(SpawnMutant), spawnInterval, spawnInterval);
    }

    private void SpawnMutant()
    {
        // Ensure the splineContainer is assigned
        if (splineContainer == null)
        {
            Debug.LogError("SplineContainer is not assigned in the Spawner script.");
            return;
        }

        // Get the exact starting position of the spline (progress = 0f)
        Vector3 spawnPosition = splineContainer.Spline.EvaluatePosition(0f);

        // Get the tangent direction at the start of the spline (progress = 0f)
        Vector3 tangentDirection = (Vector3)splineContainer.Spline.EvaluateTangent(0f);
        tangentDirection = tangentDirection.normalized;

        // Instantiate the mutant at the starting position of the spline
        GameObject mutant = Instantiate(mutantPrefab, spawnPosition, Quaternion.LookRotation(tangentDirection));

        // Attach the custom spline follower
        SplineFollower splineFollower = mutant.AddComponent<SplineFollower>();
        splineFollower.splineContainer = splineContainer; // Assign the SplineContainer
        splineFollower.speed = mutantSpeed; // Set the speed of the mutant

        // Optional: Freeze Rigidbody rotation to prevent falling over
        Rigidbody rb = mutant.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

}
