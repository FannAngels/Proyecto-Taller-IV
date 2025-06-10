using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Aquí va la putada de cuando gane :thumbsup:
            SceneManager.LoadScene("WinScreen");

            /*Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;*/

            Debug.Log("Ganaste");
        }
    }
}
