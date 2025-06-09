using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float moveSpeed;


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, moveSpeed * Time.deltaTime);
    }
}
