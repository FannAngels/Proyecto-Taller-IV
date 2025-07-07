using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Patrol Settings")] 
    public Transform[] patrolPoints;
    public float speed = 3f;
    public int targetPoint;
    public float WaitTime = 0.5f;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private Vector2 currentDirection = Vector2.right;

    [Header("Vision Settings")]
    public float visionRange = 5f;
    public float visionAngle = 45f;
    public LayerMask visionMask;

    [Header("Jumscare Settings")]
    public Image jumpscare;
    //public AudioSource jumpscareAudio;
    public float jumpscareDuration = 2f;
    public string deathScene = "LoseScreen";

    private Rigidbody2D rb;
    private GameObject player;
    public bool hasTriggeredJumpscare = false;

    public bool detectPlayer = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        targetPoint = 0;
    }

    private void Update()
    {
        if (hasTriggeredJumpscare) return;

        if (CanSeePlayer())
        {
            StartCoroutine(TriggerJumpscare());
            return;
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime; 
            if (waitTimer >= WaitTime) 
            { 
                isWaiting = false;
                waitTimer = 0f;
                increaseTargetInt();
            }
            return;
        }

        Patrol();
    }

    private void Patrol()
    {
        Transform target = patrolPoints[targetPoint];
        Vector3 direction = (target.position - transform.position).normalized;
        currentDirection = direction;


        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            isWaiting = true;
        }
    }

    void increaseTargetInt() 
    { 
        targetPoint++; 
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasTriggeredJumpscare && collision.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector2 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Too far
        if (distanceToPlayer > visionRange) return false;

        // Wrong angle
        float angle = Vector2.Angle(currentDirection, directionToPlayer);

        if (angle >visionAngle / 2f) return false;

        // View Blocked
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, visionRange, visionMask);
        if (hit.collider != null && hit.collider.CompareTag("Player")) 
        {
            return true;
        }

        return false;
    }

    public IEnumerator TriggerJumpscare()
    {
        hasTriggeredJumpscare = true;
        
        if (jumpscare != null)
        {
            jumpscare.enabled = true;
        }

        //jumpscareAudio?.Play();

        yield return new WaitForSeconds(jumpscareDuration);

        if (jumpscare != null)
        {
            jumpscare.enabled = false;
        }

        SceneManager.LoadScene("LoseScreen");
    }

    //Cone Visualization
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, currentDirection * visionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, 0, visionAngle / 2) * currentDirection;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -visionAngle / 2) * currentDirection;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftBoundary * visionRange);
        Gizmos.DrawRay(transform.position, rightBoundary * visionRange);

    }
}
