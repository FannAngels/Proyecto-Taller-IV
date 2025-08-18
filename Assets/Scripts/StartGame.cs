using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public AudioSource sound;
    //public AudioClip buttonClick;


    public void GoToLevelOne()
    {
        //sound.PlayOneShot(buttonClick);

        SceneManager.LoadScene("Level1");
    }
}
