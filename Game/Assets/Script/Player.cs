using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private TextMeshProUGUI scoreText;
    public LayerMask stopPlayerLayer;
   
    private Animator animator;
    private bool isHooping;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator> ();
    }
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space") && !isHooping)
        {
            float zDifference = 0;
            
            if(transform.position.z % 1 == 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
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
        Collider[] colliders = Physics.OverlapSphere(difference, 0.1f, stopPlayerLayer);
        if (colliders.Length == 0)
        {
            score = System.Math.Max(score,(int)transform.position.x + 1);
            animator.SetTrigger("hop");
            isHooping = true;
            transform.position += difference;
            terrainGenerator.SpawnTerrain(false,transform.position);
            scoreText.text = "Score : " + score;
        }       
    }

    public void FinishHop()
    {
        isHooping = false; 
    }
}
