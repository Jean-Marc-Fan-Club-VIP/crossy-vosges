using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    private Transform coinChildElement;

    void Start()
    {
        coinChildElement = transform.Find("CoinContainer");
    }

    void Update()
    {
        coinChildElement.transform.Rotate(0, Time.deltaTime * 100, 0);
        coinChildElement.transform.position = new Vector3(coinChildElement.transform.position.x, coinChildElement.transform.position.y + Mathf.Sin(Time.time * 2) * 0.001f, coinChildElement.transform.position.z);
    }
}
