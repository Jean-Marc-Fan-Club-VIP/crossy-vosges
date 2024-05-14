using System.Collections.Generic;
using UnityEngine;

public class RandomPlacementRowSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform spawnPos;
    public int minGameobject;
    public int maxGameobject;
    public bool isLilypad = true;
    [SerializeField] private int blockingLayer = 6;
    private readonly Collider[] colliders = new Collider[1];
    private readonly List<int> positionsAlreadyTaken = new();
    private int layerMask;
    private int numberGameobject;
    
    
    private void Start()
    {
        layerMask = 1 << blockingLayer;
        numberGameobject = Random.Range(minGameobject, maxGameobject);
        if(isLilypad)
        {
            AddObject(0);
        }
        SpawnObject();
    }
    
    private void SpawnObject()
    {
        var randomZ = Random.Range(-7, 7);
        {
            for (var i = 0; i < numberGameobject; i++)
            {
                int maxTries = 20;
                do
                {
                    maxTries--;
                    randomZ = Random.Range(-7, 7);
                    if (IsColliding(new Vector3(transform.position.x, 1f, randomZ),
                            new Vector3(0.25f, 0.25f, 0.5f), layerMask))
                    {
                        positionsAlreadyTaken.Add(randomZ);
                    }
                } while (IsTaken(randomZ) && maxTries > 0);

                if(maxTries>0)
                {
                    AddObject(randomZ);
                }
            }
        }
    }
    
    private void AddObject(int posZ)
    {
        positionsAlreadyTaken.Add(posZ);
        var randomPosition = new Vector3(spawnPos.position.x, 0.5f, posZ);
        var randomRotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
        var go = Instantiate(spawnObject, randomPosition, randomRotation);
        go.transform.parent = transform;
    }
    
    private bool IsColliding(Vector3 position, Vector3 halfExtents, int mask)
    {
        var isColliding = Physics.OverlapBoxNonAlloc(position, halfExtents, colliders,
            Quaternion.identity, mask) != 0;
        
        return isColliding;
    }
    
    private bool IsTaken(int posZ)
    {
        return positionsAlreadyTaken.Contains(posZ);
    }
}