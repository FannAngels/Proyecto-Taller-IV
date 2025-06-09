using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    public Camera cam;

    //private Animator animator;

    Vector2 movement; 
    
    //used for inputs
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        Vector2 moveDelta = new Vector2 (movement.x, movement.y);

        //flip player according to direction 
        if(moveDelta.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //deathScreenController = FindObjectOfType<DeathScreenController>();
    }

    /*void Muerte()
    {
        deathScreenController.ShowDeathScreen();
    }*/
}
