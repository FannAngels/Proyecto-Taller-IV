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
}
