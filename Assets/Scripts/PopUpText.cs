using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public class InteractableObject : MonoBehaviour
    {
        public string text = "Insert text here.";
        private bool playerDetection = false;

        private void Update()
        {
            if (playerDetection)
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
    }
}
