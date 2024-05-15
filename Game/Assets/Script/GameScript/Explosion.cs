using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (explosion != null)
        {
            Destroy(explosion);
        }
        Explode();
        if (explosion != null)
        {
            Destroy(explosion);
        }
    }

    public void Explode()
    {
        explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
