using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenuUi;

    void Update()
    {
        
    }

    public void Back()
    {
        if (PauseMenu.GameIsPaused)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
        
    }
}

