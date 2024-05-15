using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    
    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
