using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private PolygonCollider2D col;
    public Enemy enemy;

    private void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        col.isTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enemy.hasTriggeredJumpscare && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(enemy.TriggerJumpscare());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            enemy.detectPlayer = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.detectPlayer = false;
        }
    }

}
