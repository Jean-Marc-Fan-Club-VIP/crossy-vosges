using System.Collections.Generic;
using UnityEngine;

public class RandomPlacementRowSpawner : MonoBehaviour
{
    public GameObject gameobject;
    public Transform spawnPos;
    public int minGameobject;
    public int maxGameobject;
    private readonly List<int> positionsAlreadyTaken = new();
    private int numberGameobject;
    
    private void Start()
    {
        numberGameobject = Random.Range(minGameobject, maxGameobject);
        AddObject(0);
        SpawnObject();
    }
    
    private void SpawnObject()
    {
        var randomZ = 0;
        {
            for (var i = 0; i < numberGameobject; i++)
            {
                do
                {
                    randomZ = Random.Range(-7, 7);
                } while (IsTaken(randomZ));
                
                AddObject(randomZ);
            }
        }
    }
    
    private void AddObject(int posZ)
    {
        positionsAlreadyTaken.Add(posZ);
        var randomPosition = new Vector3(spawnPos.position.x, 0.5f, posZ);
        var randomRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
        var go = Instantiate(gameobject, randomPosition, randomRotation);
        var movingObject = go.GetComponent<MovingObject>();
    }
    
    private bool IsTaken(int pos)
    {
        return positionsAlreadyTaken.Contains(pos);
    }
}