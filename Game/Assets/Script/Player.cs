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

    void Start()
    {
        var transformPosition = transform.position;
        startPosition = transformPosition;
        endPosition = transformPosition;
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetKeyDown("space") && !isHooping)
        {
            float zDifference = 0;

            var transformPosition = transform.position;
            if(transformPosition.z % 1 == 0)
            {
                zDifference = Mathf.Round(transformPosition.z) - transformPosition.z;
            }
            MoveCharacter(new Vector3(1,0,zDifference));
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && !isHooping)
        {
            MoveCharacter((new Vector3(0, 0, 1)));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isHooping)
        {
            MoveCharacter((new Vector3(0, 0, -1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isHooping)
        {
            MoveCharacter((new Vector3(-1, 0, 0)));
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

    private void MoveCharacter(Vector3 difference)
    {
        var newPosition = transform.position + difference;
        var colliders = Physics.OverlapBox(newPosition, new Vector3(0.3f,0.3f,0.3f), Quaternion.identity, -1);
        var newColliders = colliders.Where((c => c.CompareTag("Obstacle"))).ToArray();
        if (newColliders.Length == 0)
        {
            startPosition = transform.position;
            endPosition = transform.position + difference;
            score = System.Math.Max(score,(int)transform.position.x + 1);
            animator.SetTrigger("hop");
            isHooping = true;
            terrainGenerator.SpawnTerrain(false,transform.position);
            scoreText.text = "Score : " + score;
        }
    }

    public void FinishHop()
    {
        isHooping = false; 
    }
}
