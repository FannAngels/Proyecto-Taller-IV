using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public bool imageOne = false;
    public bool imageTwo = false;
    public Button finishButton;

    public void Start()
    {
        finishButton.gameObject.SetActive(false);
    }

    private bool WinningCondition()
    {
        if (imageOne == true && imageTwo == true)
        {
            return true;
        }
        else
        {
            return false;
        }          
    }

    public void ImageDone(int puzzle)
    {
        switch (puzzle)
        {
            case 1:
                imageOne = true;
                break;
            case 2: 
                imageTwo = true;
                break;
        }

        if (WinningCondition())
        {
            finishButton.gameObject.SetActive(true);
        }
    }

    public void FinishGame()
    {
        GameJolt.API.Trophies.Unlock(277236, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log("Something went wrong");
            }
        });
        SceneManager.LoadScene("WinScreen");
    }


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Aquí va la putada de cuando gane :thumbsup:
            SceneManager.LoadScene("WinScreen");

            *//*Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;*//*

            Debug.Log("Ganaste");
        }
    }*/
}
