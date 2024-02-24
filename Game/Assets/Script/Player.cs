using System.Linq;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private TextMeshProUGUI scoreText;
    private Animator animator;
    private bool isHooping;
    private int score;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private const float DesiredDuration = 0.1f;
    private float elapsedTime;

    private void Start()
    {
        var transformPosition = transform.position;
        startPosition = transformPosition;
        endPosition = transformPosition;
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        var currentPosition = transform.position;
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
        if (Input.GetKeyDown("space") && !isHooping)
        {
            float zDifference = 0;

            if(currentPosition.z % 1 == 0)
            {
                zDifference = Mathf.Round(currentPosition.z) - currentPosition.z;
            }
            MoveCharacter(currentPosition,new Vector3(1,0,zDifference));
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && !isHooping)
        {
            MoveCharacter(currentPosition,(new Vector3(0, 0, 1)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHooping)
        {
            MoveCharacter(currentPosition,(new Vector3(0, 0, -1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHooping)
        {
            MoveCharacter(currentPosition,(new Vector3(-1, 0, 0)));
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<MovingObject>() != null)
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
        var colliders = Physics.OverlapBox(newPosition, new Vector3(0.3f,0.3f,0.3f), Quaternion.identity, -1);
        var newColliders = colliders.Where((c => c.CompareTag("Obstacle"))).ToArray();
        if (newColliders.Length == 0)
        {
            startPosition = currentPosition;
            endPosition = currentPosition + difference;
            score = System.Math.Max(score,(int)currentPosition.x + 1);
            animator.SetTrigger("hop");
            isHooping = true;
            terrainGenerator.SpawnTerrain(false,currentPosition);
            scoreText.text = "Score : " + score;
        }
    }

    public void FinishHop()
    {
        isHooping = false; 
    }
}
