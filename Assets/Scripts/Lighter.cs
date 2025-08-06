using UnityEngine;

public class Lighter : MonoBehaviour
{
    public bool hasLighter = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Lighter") && Input.GetKeyDown(KeyCode.E))
        {
            hasLighter = true;
            Destroy(other.gameObject);
        }
    }
}
