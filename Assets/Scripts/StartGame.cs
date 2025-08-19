using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //public AudioSource sound;
    //public AudioClip buttonClick;
    public GameObject startButton;

    public void Start()
    {
        var isSignedIn = GameJoltAPI.Instance.CurrentUser != null;
        if (isSignedIn)
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
        }
        //GameJoltAPI.Instance.CurrentUser.SignOut();
        //GameJoltUI.Instance.ShowSignIn();
        GameJoltUI.Instance.ShowSignIn((bool signInSuccess) => { startButton.SetActive(true); });
    }

    public void CheckLogin()
    {

    }



    public void GoToLevelOne()
    {
        //sound.PlayOneShot(buttonClick);

        SceneManager.LoadScene("Level1");
    }
}
