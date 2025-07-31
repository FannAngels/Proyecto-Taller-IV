using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Rotation rotation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entré");
            collision.TryGetComponent(out Enemy enemy);
            enemy.rotation = rotation;
            Debug.Log($"{enemy}");
        }
    }
}
