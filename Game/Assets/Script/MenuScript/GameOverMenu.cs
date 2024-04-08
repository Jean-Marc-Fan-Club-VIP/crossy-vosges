using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;
    private AudioControler audioController;

    public static bool GoGameOverMenu = false; 

    private void Start()
    {
        GoGameOverMenu = false;
    }

    private void Update()
    {
        if (GoGameOverMenu)
        {
            audioController = FindObjectOfType<AudioControler>();

            if (audioController != null)
            {
                audioController.PauseBackgroundMusic();
            }
            gameOverMenuUi.SetActive(true);
            GoGameOverMenu = true;
            Time.timeScale = 0f;
        }
    }

    public void ReplayGame()
    {
        if (audioController != null)
        {
            audioController.ResumeBackgroundMusic();
        }
        GoGameOverMenu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        GoGameOverMenu = false;
        if (audioController != null)
        {
            audioController.ResumeBackgroundMusic();
        }
        Debug.Log("Back Start Menu");
        SceneManager.LoadScene(1);
    }
}