using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic; 

public class InteractableObject : MonoBehaviour
{
    public string text = "Insert text here.";
    private bool playerDetection = false;

    private void Update()
    {
        if (playerDetection && Input.GetKeyDown(KeyCode.E))
        {
            TextUI.Instance.ShowText(text);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetection = false;
            TextUI.Instance.HideText();
        }
    }

    /*private Collider2D collider;
    [SerializeField] private ContactFilter2D filter;
    private List<Collider2D> collObjs = new List<Collider2D>(1); 

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        collider.Overlap(filter, collObjs);
        foreach (var o in collObjs)
        {
            Debug.Log("Collided with " + o.name);
        };
    }*/

}
