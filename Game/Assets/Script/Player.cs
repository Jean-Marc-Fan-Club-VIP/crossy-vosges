﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private const float DesiredDuration = 0.1f;

    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int blockingLayer = 6;

    private readonly Collider[]
        colliders = new Collider[1]; // We only need one collider to make the collision detection work. Improves performances

    private Animator animator;
    private int blockingLayerMask;
    private float elapsedTime;
    private Vector3 endPosition;
    private bool isHooping;
    private int score;
    private bool canMove;
    private Vector3 startPosition;

    private void Start()
    {
        canMove = true;
        blockingLayerMask = 1 << blockingLayer;
        var transformPosition = transform.position;
        startPosition = transformPosition;
        endPosition = transformPosition;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (startPosition != endPosition)
        {
            var isOnLog = !IsColliding(transform.position, new Vector3(0.1f, 0.5f, 0.1f), blockingLayerMask);
            if (!isOnLog)
            {
                endPosition.x = (float)Math.Round(endPosition.x);
                endPosition.z = (float)Math.Round(endPosition.z);
            }

            elapsedTime += Time.deltaTime;
            var percentageComplete = elapsedTime / DesiredDuration;
            transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            if (percentageComplete >= 1.0f)
            {
                startPosition = endPosition;
                elapsedTime = 0;
            }
        }

        var currentPosition = transform.position;
        if (currentPosition.y <= -1)
        {
            Destroy(gameObject);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.Space)) && !isHooping && canMove)
        {
            MoveCharacter(currentPosition, Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHooping && canMove)
        {
            MoveCharacter(currentPosition, Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHooping && canMove)
        {
            MoveCharacter(currentPosition, Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHooping && canMove)
        {
            MoveCharacter(currentPosition, Vector3.left);
        }
    }

    public void OnDestroy()
    {
        GameOverMenu.GoGameOverMenu = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collision with the terrain
        canMove = true;

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
        if (isMoveBlocked)
        {
            return;
        }

        startPosition = currentPosition;
        endPosition = currentPosition + difference;
        endPosition.y += 0.05f; // Make sure to leave the ground
        score = Math.Max(score, (int)currentPosition.x + 1);
        scoreText.text = $"Score : {score}";
        animator.SetTrigger("hop");
        isHooping = true;
        canMove = false;
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
}