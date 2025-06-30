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
    private bool hasTriggeredJumpscare = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");

        targetPoint = 0;
    }

    private void Update()
    {
        /*if (hasTriggeredJumpscare) return;

        if (CanSeePlayer())
        {
            StartCoroutine(TriggerJumpscare())
        }*/

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
}
