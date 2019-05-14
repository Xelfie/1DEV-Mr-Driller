using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.position.y, -15);
    }
}
