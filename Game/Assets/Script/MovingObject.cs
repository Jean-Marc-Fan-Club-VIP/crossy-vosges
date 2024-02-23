using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private float speed;
    public float Speed
    {
        set => speed = value;
    }
    public bool isLog;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z < -25 || transform.position.z > 25)
        {
            Destroy(gameObject);
        }
    }

}
