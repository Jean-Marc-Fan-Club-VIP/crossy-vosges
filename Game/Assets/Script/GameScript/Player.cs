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

        var currentPosition = transform.position;
        if (currentPosition.y <= -1)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            MoveCharacter(currentPosition, Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCharacter(currentPosition, Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCharacter(currentPosition, Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCharacter(currentPosition, Vector3.left);
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
        //sound every 50 points
        StartCoroutine(ControlScorePlayer()); 

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

    IEnumerator ControlScorePlayer()
    {
        if ((score%50 == 0) && (score > 0))
        {
            if (!soundScoreIsPlayed)
            {
                if (sound && audioSource)
                {
                    audioSource.volume = OptionsMenu.volumeSound;
                    audioSource.PlayOneShot(sound);
                    yield return new WaitForSeconds(sound.length);
                }
                soundScoreIsPlayed = true;
            }
        }
        else
        {
            soundScoreIsPlayed = false;
        }
    }
}