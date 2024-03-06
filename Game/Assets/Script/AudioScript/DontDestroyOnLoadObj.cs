using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadObj : MonoBehaviour
{
    private void Awake()
    {
        // Ensure that this GameObject persists through scene changes
        DontDestroyOnLoad(gameObject);
    }
}
