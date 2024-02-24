using System.Collections;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private int minSeparationtime;
    [SerializeField] private int maxSeparationTime;
    [SerializeField] private bool isRightSide;

    private float rowSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        rowSpeed = Random.Range(2.0f, 6.0f);
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minSeparationtime, maxSeparationTime));
            var go = Instantiate(spawnObject, spawnPos.position, Quaternion.identity);
            go.GetComponent<MovingObject>().Speed = rowSpeed;
            if(!isRightSide && go ) 
            {
                go.transform.Rotate(new Vector3(0,180,0));
            }
        }
    }
}
