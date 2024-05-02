using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadSpawner : MonoBehaviour
{
    public GameObject lilypad;
    public Transform spawnPos;
    public int minNumbersLilypad;
    public int maxNumbersLilypad;
    private List<int> positionsAlreadyTaken = new List<int>();
    int numberOfLilypads ; 

    void Start()
    {
        numberOfLilypads = Random.Range(minNumbersLilypad, maxNumbersLilypad);

        addObject(0);

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
                    randomZ = (int)Random.Range(-7, 7); 
                } while (isTaken(randomZ)); 

                addObject(randomZ);
                
            }
        }
    }

    private void addObject(int posZ)
    {
        positionsAlreadyTaken.Add(posZ);
        Vector3 randomPosition = new Vector3(spawnPos.position.x, 0.5f, posZ);
        Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
        var go = Instantiate(lilypad, randomPosition, randomRotation);
        var movingObject = go.GetComponent<MovingObject>();
    }

    private bool isTaken(int pos)
    {
        return positionsAlreadyTaken.Contains(pos);
    }

}
