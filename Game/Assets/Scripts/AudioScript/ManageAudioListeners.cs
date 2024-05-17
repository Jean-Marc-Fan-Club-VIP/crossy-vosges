using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAudioListeners : MonoBehaviour
{
    void Start()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        for (int i = 1; i < listeners.Length; i++)
        {
            listeners[i].enabled = false;
        }
    }

}
