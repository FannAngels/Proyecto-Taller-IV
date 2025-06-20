using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip buttonClick;

    public void goToMenu()
    {
        sound.PlayOneShot(buttonClick);
        Invoke("LoadScene", buttonClick.length);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
