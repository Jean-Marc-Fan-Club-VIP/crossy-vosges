using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAnimation : MonoBehaviour
{
    private Transform logChildElement;
    private float initialPositionY;
    private bool playAnimationIn = false;
    private bool playAnimationOut = false;
    private const float AnimationDuration = 0.25f;
    private float animationTime = 0;

    void Start()
    {
        logChildElement = transform.Find("Rondin");
        initialPositionY = logChildElement.transform.position.y;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Player>())
        {
            playAnimationIn = true;
            playAnimationOut = false;
            animationTime = 0;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.GetComponent<Player>())
        {
            playAnimationIn = false;
            playAnimationOut = true;
            animationTime = 0;
        }
    }

    void Update()
    {
        // Rotation
        float sineRotate = Mathf.Sin(Time.time * 2) * 0.1f * Time.timeScale;
        logChildElement.transform.Rotate(sineRotate * 1.5f, sineRotate, 0);

        // Animation
        if(playAnimationIn || playAnimationOut)
        {
            float animationIntensity = 0.3f;
            animationTime += Time.deltaTime;
            float t = animationTime / AnimationDuration;
            if(t > 0.5f)
            {
                t = 1 - t;
            }
            
            if (playAnimationIn)
            {
                logChildElement.transform.position = new Vector3(logChildElement.transform.position.x,
                    Mathf.Lerp(initialPositionY, initialPositionY - animationIntensity, t),
                    logChildElement.transform.position.z);
            }
            else
            {
                logChildElement.transform.position = new Vector3(logChildElement.transform.position.x,
                    Mathf.Lerp(initialPositionY, initialPositionY + animationIntensity, t),
                    logChildElement.transform.position.z);
            }
            if (t <= 0)
            {
                playAnimationIn = false;
                playAnimationOut = false;
            }
        }
    }
}
