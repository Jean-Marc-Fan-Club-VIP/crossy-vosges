using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLoadScene : MonoBehaviour
{
    public int sceneToLoad = 1;

    void Start()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
