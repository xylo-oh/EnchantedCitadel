using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    public SplineContainer splineContainer; // Reference to the SplineContainer
    public float speed = 2f; // Speed of movement along the spline
    private float progress = 0f; // Progress along the spline (0 to 1)

    private void Update()
    {
        if (splineContainer == null) return;

        // Move along the spline based on speed and time
        progress += speed * Time.deltaTime / splineContainer.CalculateLength();
        if (progress > 1f) progress = 0f; // Loop back to the start if progress exceeds 1

        // Get the position on the spline based on progress
        Vector3 position = splineContainer.Spline.EvaluatePosition(progress);
        transform.position = position;

        // Align the mutant's forward direction with the spline's tangent
        Vector3 tangent = splineContainer.Spline.EvaluateTangent(progress);
        if (tangent != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(tangent);
        }
    }
}
