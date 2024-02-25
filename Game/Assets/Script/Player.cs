using System;
using TMPro;
using UnityEngine;

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
    private Vector3 startPosition;

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Space) && !isHooping)
        {
            float zDifference = 0;
            if (currentPosition.z % 1 == 0) zDifference = Mathf.Round(currentPosition.z) - currentPosition.z;
            MoveCharacter(currentPosition, new Vector3(1, 0, zDifference));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isHooping)
        {
            MoveCharacter(currentPosition, Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHooping)
        {
            MoveCharacter(currentPosition, Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHooping)
        {
            MoveCharacter(currentPosition, Vector3.left);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<MovingObject>() != null)
        {
            if (collision.collider.GetComponent<MovingObject>().isLog) transform.parent = collision.collider.transform;
        }
        else
        {
            transform.parent = null;
        }
    }

    private void MoveCharacter(Vector3 currentPosition, Vector3 difference)
    {
        var newPosition = currentPosition + difference;
        var isColliding = Physics.OverlapBoxNonAlloc(newPosition, new Vector3(0.3f, 0.3f, 0.3f), colliders,
            Quaternion.identity, blockingLayerMask) != 0;
        if (isColliding) return;
        startPosition = currentPosition;
        endPosition = currentPosition + difference;
        score = Math.Max(score, (int)currentPosition.x + 1);
        scoreText.text = $"Score : {score}";
        animator.SetTrigger("hop");
        isHooping = true;
        terrainGenerator.SpawnTerrain(false, currentPosition);
    }

    public void FinishHop()
    {
        isHooping = false;
    }
}