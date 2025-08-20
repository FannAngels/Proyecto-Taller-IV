using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    public Camera cam;

    private Animator animator;

    Vector2 movement;

    private readonly int idle = Animator.StringToHash("Idle");
    private readonly int walkUp = Animator.StringToHash("WalkUp");
    private readonly int walkDown = Animator.StringToHash("WalkDown");
    private readonly int walkRight = Animator.StringToHash("WalkRight");

    private PlayerInput playerInput;
    public PlayerController playerController;
    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        playerController = new();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerInput.currentActionMap = playerController.Player;
        //deathScreenController = FindObjectOfType<DeathScreenController>();
    }

    //used for inputs
    void Update()
    {
        /* movement.x = Input.GetAxisRaw("Horizontal");
         movement.y = Input.GetAxisRaw("Vertical");*/
        movement.x = playerController.Player.Move.ReadValue<Vector2>().x; 
        movement.y = playerController.Player.Move.ReadValue<Vector2>().y;
        moveInput.Normalize();

        Vector2 moveDelta = new Vector2 (movement.x, movement.y).normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        /*if (moveDelta.x != 0 && moveDelta.y != 0)
        {
            animator.SetBool(idle, false);
        } 

        if (moveDelta.y > 0)
        {
            animator.SetBool (walkRight, false);
            animator.SetBool(walkDown, false);
            animator.SetBool(walkUp, true);
        }

        if (moveDelta.y < 0)
        {
            animator.SetBool(walkRight, false);
            animator.SetBool(walkDown, true);
            animator.SetBool(walkUp, false);
        }

        if (moveDelta.x != 0)
        {
            animator.SetBool(walkUp, false);
            animator.SetBool(walkDown, false);
            animator.SetBool(walkRight, true);

            //flip player according to direction 
            if (moveDelta.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (moveDelta.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {

        }*/


        //Collision check

        /*animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento.y);
        animator.SetFloat("Velocidad", movimiento.sqrMagnitude);*/
    }

    //used for movement
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position +  movement * moveSpeed * Time.fixedDeltaTime);
    }

    

    /*void Muerte()
    {
        deathScreenController.ShowDeathScreen();
    }*/
}
