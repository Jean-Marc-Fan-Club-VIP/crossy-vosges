using System.Collections.Generic;
using UnityEngine;

public class RandomPlacementRowSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform spawnPos;
    public int minGameobject;
    public int maxGameobject;
    [SerializeField] private int blockingLayer = 6;
    private readonly Collider[] colliders = new Collider[1];
    private readonly List<int> positionsAlreadyTaken = new();
    private int layerMask;
    private int numberGameobject;
    
    
    private void Start()
    {
        layerMask = 1 << blockingLayer;
        numberGameobject = Random.Range(minGameobject, maxGameobject);
        AddObject(0);
        SpawnObject();
    }
    
    private void SpawnObject()
    {
        var randomZ = 0;
        // var boxCliider=spawnObject.GetComponent<BoxCollider>();
        {
            for (var i = 0; i < numberGameobject; i++)
            {
                do
                {
                    randomZ = Random.Range(-7, 7);
                    if (!IsColliding(new Vector3(transform.position.x, 0.5f, randomZ),
                            new Vector3(0.25f, 0.24f, 0.5f), layerMask))
                    {
                        positionsAlreadyTaken.Add(randomZ);
                        break;
                    }
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
        Instantiate(spawnObject, randomPosition, randomRotation);
    }
    
    private bool IsColliding(Vector3 position, Vector3 halfExtents, int mask)
    {
        var isColliding = Physics.OverlapBoxNonAlloc(position, halfExtents, colliders,
            Quaternion.identity, mask) != 0;
        
        Debug.Log($"isColliding: {isColliding} Position: {position}");
        return isColliding;
    }
    
    private bool IsTaken(int posZ)
    {
        return positionsAlreadyTaken.Contains(posZ);
    }
}