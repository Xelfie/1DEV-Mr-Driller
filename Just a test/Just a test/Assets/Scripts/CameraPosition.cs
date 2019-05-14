using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    
    void LateUpdate()
    {
        if (player.position.y < transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, player.position.y, -15);
            transform.position = newPosition;
        }
    }
}
