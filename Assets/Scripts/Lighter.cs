using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lighter : MonoBehaviour
{
    public bool hasLighter = false;
    public Light2D characterLight; // Drag your child light object in the inspector

    private void Start()
    {
        characterLight.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Lighter") && Input.GetKeyDown(KeyCode.E))
        {
            hasLighter = true;
            Destroy(other.gameObject);
            characterLight.enabled = true;
        }
    }
}
