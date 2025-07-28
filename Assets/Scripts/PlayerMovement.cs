using UnityEngine;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //deathScreenController = FindObjectOfType<DeathScreenController>();
    }

    //used for inputs
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        Vector2 moveDelta = new Vector2 (movement.x, movement.y);

        if (moveDelta.x != 0 && moveDelta.y != 0)
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

        }
       

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
