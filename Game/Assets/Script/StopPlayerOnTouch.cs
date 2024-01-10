using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            collision.gameObject.transform.position = collision.gameObject.transform.position + new Vector3(-1,0,0); 
        }

        
    }
}
