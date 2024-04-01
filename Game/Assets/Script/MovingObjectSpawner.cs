using System.Collections;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private int minSeparationtime;
    [SerializeField] private int maxSeparationTime;
    [SerializeField] private bool isRightSide;

    private float rowSpeed;

    private void Start()
    {
        rowSpeed = Random.Range(2.0f, 6.0f);
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSeparationtime, maxSeparationTime));
            
            // Select random
            var spawnObject = this.spawnObjects[Random.Range(0, this.spawnObjects.Length)];
            
            var go = Instantiate(spawnObject, spawnPos.position, Quaternion.identity);
            var movingObject = go.GetComponent<MovingObject>();
            
            // var rowSize = gameObject.GetComponent<MeshRenderer>().bounds.size.z;
            var rowSize = 50;
            
            var speedMultiplier = 1f;
            if (movingObject.isLog)
            {
                speedMultiplier = 0.75f;
            }

            movingObject.speed = rowSpeed * speedMultiplier;
            movingObject.leftBound = -(rowSize / 2);
            movingObject.rightBound = rowSize / 2;
            if (!isRightSide && go)
            {
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}