using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class StartPuzzle : MonoBehaviour
{
    private GameObject player;

    [Header("Interaction Settings")]
    [SerializeField] public float interactionDistance = 1.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (PlayerMovement.instance.playerController.Player.Interact.WasPressedThisFrame())
        {
            StartPuzzleMiniGame();
        }

    }
        private void StartPuzzleMiniGame()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > interactionDistance)
        {
            return;
        }
        else
        {
            SceneManager.LoadScene("Puzzle");
        }

    }

    //sound.PlayOneShot(buttonClick);      
}






    /*public PlayerMovement controller;
    public GameObject puzzle;

    [Header("Interaction Settings")]
    [SerializeField] public float interactionDistance = 1.0f;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerMovement>();
        puzzle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartPuzzleMiniGame();
        }
    }

    private void StartPuzzleMiniGame()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > interactionDistance)
        {
            return;
        }
        else
        {
            puzzle.SetActive(true);

            controller.enabled = false;
        }

    }*/

