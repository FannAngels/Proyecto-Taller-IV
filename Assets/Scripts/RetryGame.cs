using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public void GoToLevelOne()
    {
        //sound.PlayOneShot(buttonClick);

        SceneManager.LoadScene("Level1");
    }
}
