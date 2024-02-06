using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;

    void Update()
    {
        
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
