using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float cameraDistance = 5f; 
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
      
        Vector3 targetPosition = player.position - player.forward * cameraDistance;

        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

       
        transform.LookAt(player);
    }
}