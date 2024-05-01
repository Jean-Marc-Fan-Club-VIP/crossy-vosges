using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadSpawner : MonoBehaviour
{
    public GameObject lilypad;
    public Transform spawnPos;
    public int minSeparationtime;
    public int maxSeparationTime;
    private List<int> positionsAlreadyTaken = new List<int>();
    int numberOfLilypads ; 
    float waterWidth = 50;

    void Start()
    {
        positionsAlreadyTaken.Add(0);
        numberOfLilypads = Random.Range(2, 4);
        SpawnLilypad();
    }

    private void SpawnLilypad()
    {
        int randomZ = 0;
        {
            for (int i = 0; i < numberOfLilypads; i++)
            {
                do
                {
                    randomZ = (int)Random.Range(-(waterWidth/2)+15, (waterWidth / 2) - 15); 
                } while (isTaken(randomZ)); 

                positionsAlreadyTaken.Add(randomZ);
                Vector3 randomPosition = new Vector3(spawnPos.position.x, 0.5f, randomZ);
 
                var go = Instantiate(lilypad, randomPosition, Quaternion.identity);
                var movingObject = go.GetComponent<MovingObject>();
                
            }
        }
    }

    private bool isTaken(int pos)
    {
        return positionsAlreadyTaken.Contains(pos);
    }

}
