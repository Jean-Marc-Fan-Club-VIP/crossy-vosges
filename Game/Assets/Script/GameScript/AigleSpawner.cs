using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AigleSpawner : MonoBehaviour
{
    public GameObject aigle;
    public GameObject player;
    public static bool isReady;
    private float speed = 5f;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 3f,  -1f);
        isReady = false;
        
    }

    void Update()
    {
        if(player != null)
        {
            if (isReady)
            {
                float deltaX = speed * Time.deltaTime;
                transform.position += new Vector3(deltaX, 0f, 0f);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x - 12f, 3f, -1f);
            }
            if (transform.position.x > player.transform.position.x + 9)
            {
                Destroy(gameObject);
                Destroy(player);
            }

            // check if player and aigle is in same x position
            int playerX = (int)player.transform.position.x;
            int aigleX = (int)transform.position.x;

            if (Mathf.Approximately(playerX, aigleX))
            {
                SetPlayerTransparency(); 
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void SetPlayerTransparency()
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }
}
