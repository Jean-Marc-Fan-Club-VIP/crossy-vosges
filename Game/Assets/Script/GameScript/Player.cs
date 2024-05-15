using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float LerpDesiredDurationMove = 0.1f;

    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private int blockingLayer = 6;

    private readonly Collider[]
        colliders = new Collider[1]; // We only need one collider to make the collision detection work. Improves performances

    private Animator animator;
    private int blockingLayerMask;
    private bool canMove;
    private Vector3 endPosition;
    private Quaternion endRotation;
    private bool isHooping;
    private float lerpElapsedTime;
    private int score;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float timerAntiMoveLock;

    private bool soundScoreIsPlayed;
    public AudioClip sound;
    private AudioSource audioSource;

    private int previousScore; 
    private Vector3 previousPosition; 
    private float previousTimerValue;
    private float immobileTime = 0f;
    private Vector2 mobileStartTouchPosition;
    private Vector2 mobileEndTouchPosition;


    private void Start()
    {
        canMove = true;
        blockingLayerMask = 1 << blockingLayer;
        startPosition = transform.position;
        endPosition = transform.position;
        startRotation = transform.rotation;
        endRotation = transform.rotation;
        animator = GetComponent<Animator>();
        soundScoreIsPlayed = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.ScoreUpdated += ControlScoreSound;
        EventManager.ScoreUpdated += UpdatePreviousData;
        EventManager.TimerUpdated += ControlPlayerMovement;
        EventManager.TimerUpdated += ControlMoveBackPlayer;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= ControlScoreSound;
        EventManager.ScoreUpdated -= UpdatePreviousData;
        EventManager.TimerUpdated -= ControlPlayerMovement;
        EventManager.TimerUpdated -= ControlMoveBackPlayer;
    }

    private void Update()
    {
        if (startPosition != endPosition || startRotation != endRotation)
        {
            var isOnLog = !IsColliding(transform.position, new Vector3(0.25f, 1f, 0.5f), blockingLayerMask);
            if (!isOnLog)
            {
                endPosition.x = (float)Math.Round(endPosition.x);
                endPosition.z = (float)Math.Round(endPosition.z);
            }

            lerpElapsedTime += Time.deltaTime;
            var percentageComplete = lerpElapsedTime / LerpDesiredDurationMove;
            transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, percentageComplete);
            if (percentageComplete >= 1.0f)
            {
                startPosition = endPosition;
                startRotation = endRotation;
                lerpElapsedTime = 0;
            }
        }

        // Prevent move locking
        timerAntiMoveLock += Time.deltaTime;
        if (timerAntiMoveLock >= 0.5f)
        {
            canMove = true;
            isHooping = false;
        }

        // Prevent from going out of the map on the sides
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -8f, 10f));

        var currentPosition = transform.position;
        if (currentPosition.y <= -1)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Z))
        {
            MoveCharacter(currentPosition, Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
        {
            MoveCharacter(currentPosition, Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter(currentPosition, Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveCharacter(currentPosition, Vector3.left);
        }

        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Mobile controls
            mobileStartTouchPosition = Input.GetTouch(0).position;
        }
        else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Mobile controls
            mobileEndTouchPosition = Input.GetTouch(0).position;

            var difference = mobileEndTouchPosition - mobileStartTouchPosition;
            if (difference.magnitude > 25)
            {
                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                {
                    if (difference.x > 0)
                    {
                        MoveCharacter(currentPosition, Vector3.back);
                    }
                    else
                    {
                        MoveCharacter(currentPosition, Vector3.forward);
                    }
                }
                else
                {
                    if (difference.y > 0)
                    {
                        MoveCharacter(currentPosition, Vector3.right);
                    }
                    else
                    {
                        MoveCharacter(currentPosition, Vector3.left);
                    }
                }
            }
            else
            {
                MoveCharacter(currentPosition, Vector3.right);
            }
        }

    }

    public void OnDestroy()
    {
        EventManager.OnGameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collision with the terrain
        canMove = true;

        if(collision.gameObject.layer == blockingLayer){
            // Recenter player when leaving a log
            startPosition = transform.position;
            endPosition = new Vector3((float)Math.Round(transform.position.x), transform.position.y, (float)Math.Round(transform.position.z));
        }

        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().isLog)
            {
                transform.parent = collision.collider.transform;
            }
        }
        else
        {
            transform.parent = null;
        }
    }

    private void MoveCharacter(Vector3 currentPosition, Vector3 difference)
    {
        var newPosition = currentPosition + difference;
        var isMoveBlocked = IsColliding(newPosition, new Vector3(0.3f, 0.3f, 0.3f), blockingLayerMask);
        if (isMoveBlocked || !canMove || isHooping || newPosition.x < -0.5)
        {
            return;
        }

        startPosition = currentPosition;
        endPosition = newPosition;

        // Make sure to leave the ground
        startPosition.y += 0.04f;
        endPosition.y += 0.04f;

        startRotation = transform.rotation;
        if (difference == Vector3.right)
        {
            endRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (difference == Vector3.left)
        {
            endRotation = Quaternion.Euler(0, 179.999f, 0);
        }
        else if (difference == Vector3.forward)
        {
            endRotation = Quaternion.Euler(0, 270.001f, 0);
        }
        else if (difference == Vector3.back)
        {
            endRotation = Quaternion.Euler(0, 90, 0);
        }

        score = Math.Max(score, (int)currentPosition.x + 1);
         

        EventManager.UpdateScore(score);
        animator.SetTrigger("hop");
        isHooping = true;
        canMove = false;
        timerAntiMoveLock = 0;
        terrainGenerator.SpawnTerrain(false, currentPosition);
    }

    private bool IsColliding(Vector3 position, Vector3 halfExtents, int mask)
    {
        var isColliding = Physics.OverlapBoxNonAlloc(position, halfExtents, colliders,
            Quaternion.identity, mask) != 0;
        return isColliding;
    }

    public void FinishHop()
    {
        isHooping = false;
    }

    void ControlScoreSound(int scoreValue)
    {
        if ((scoreValue%50 == 0) && (scoreValue > 0))
        {
            if (!soundScoreIsPlayed)
            {
                if (sound && audioSource)
                {
                    audioSource.volume = OptionsMenu.volumeSound;
                    audioSource.PlayOneShot(sound);
                }
                soundScoreIsPlayed = true;
            }
        }
        else
        {
            soundScoreIsPlayed = false;
        }
    }

    private void ControlMoveBackPlayer(float timeValue)
    {
       if ((int)transform.position.x == (score - 5))
        {
            PlayerDisqualify();
        }
    }

    private void ControlPlayerMovement(float timeValue)
    {
        bool hasPlayerMoved = false;

        if (score != previousScore || transform.position != previousPosition)
        {
            hasPlayerMoved = true;
        }
        if (!hasPlayerMoved)
        {
            immobileTime += Time.deltaTime;
            if (immobileTime >= 5f)
            {
                PlayerDisqualify();
            }
        }
        else
        {
            immobileTime = 0f;
        }

        previousScore = score;
        previousPosition = transform.position;
    }

    private void UpdatePreviousData(int scoreValue)
    {
        previousScore = scoreValue;
        previousPosition = transform.position;
    }

    private void PlayerDisqualify()
    {
        EagleSpawner.isReady = true;
    }
}