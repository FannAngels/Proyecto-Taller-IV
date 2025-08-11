using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lighter : MonoBehaviour
{
    public bool hasLighter { get; private set; } = false;
    [SerializeField]public Light2D characterLight; // Drag your child light object in the inspector
    [SerializeField] private string pickupTag = "Lighter";

    private GameObject pickup = null;

    private void Awake()
    {
        if (characterLight != null) characterLight.enabled = false;
    }

    private void Update()
    {
        // If we're near a pickup and press E, pick it up
        if (!hasLighter && pickup != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        if (pickup == null) return;

        hasLighter = true;
        Destroy(pickup);         // remove the world pickup
        pickup = null;

        if (characterLight != null) characterLight.enabled = true;

        // Optional: notify other systems, raise an event, play SFX, UI update, etc.
    }


    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Lighter") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Agarraste el encendedor");
            hasLighter = true;
            Destroy(other.gameObject);
            characterLight.enabled = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(pickupTag))
        {
            pickup = other.gameObject;
            // Optional: show UI "Press E to pick up"
            // Debug.Log("Lighter in range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(pickupTag) && other.gameObject == pickup)
        {
            pickup = null;
            // Optional: hide UI
            // Debug.Log("Lighter out of range");
        }
    }
}

/*public class Lighter : MonoBehaviour
{
    public bool hasLighter { get; private set; } = false;
    [SerializeField] private Light2D characterLight;      // assign in Inspector
    [SerializeField] private string pickupTag = "Lighter"; // tag of the lighter pickup object

    private GameObject nearbyPickup = null; // currently overlapped pickup

    private void Awake()
    {
        if (characterLight != null) characterLight.enabled = false;
    }

    private void Update()
    {
        // If we're near a pickup and press E, pick it up
        if (!hasLighter && nearbyPickup != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (nearbyPickup == null) return;

        hasLighter = true;
        Destroy(nearbyPickup);         // remove the world pickup
        nearbyPickup = null;

        if (characterLight != null) characterLight.enabled = true;

        // Optional: notify other systems, raise an event, play SFX, UI update, etc.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(pickupTag))
        {
            nearbyPickup = other.gameObject;
            // Optional: show UI "Press E to pick up"
            // Debug.Log("Lighter in range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(pickupTag) && other.gameObject == nearbyPickup)
        {
            nearbyPickup = null;
            // Optional: hide UI
            // Debug.Log("Lighter out of range");
        }
    }
}*/