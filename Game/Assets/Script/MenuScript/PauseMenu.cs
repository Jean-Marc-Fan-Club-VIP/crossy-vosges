using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;


    private AudioControler audioController;

    void Start()
    {
        audioController = FindObjectOfType<AudioControler>();
        Resume();
        

        if (audioController == null)
        {
            Debug.LogWarning("AudioControler non trouvé dans la scène.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        if (audioController != null)
        {
            audioController.ResumeBackgroundMusic();
        }
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        if (audioController != null)
        {
            audioController.PauseBackgroundMusic();
        }
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RePlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        if (audioController != null)
        {
            audioController.ResumeBackgroundMusic();
        }
        Debug.Log("Back Start Menu");
        SceneManager.LoadScene(1);
    }

    public void OptionsGame()
    {
        pauseMenuUi.SetActive(false);
        OptionsMenu.GoOptions = true;
    }
}