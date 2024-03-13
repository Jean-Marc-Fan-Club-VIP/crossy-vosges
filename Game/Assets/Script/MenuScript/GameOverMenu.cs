using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;
    private AudioControler audioController;

    private void Start()
    {
        audioController = FindObjectOfType<AudioControler>();

        if (audioController != null)
        {
            // Appeler la méthode PauseBackgroundMusic() de l'objet AudioControler
            audioController.PauseBackgroundMusic();
        }
        else
        {
            Debug.LogWarning("AudioControler non trouvé dans la scène.");
        }
    }

    public void ReplayGame()
    {
        audioController.ResumeBackgroundMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        audioController.ResumeBackgroundMusic();
        Debug.Log("Back Start Menu");
        SceneManager.LoadScene(1);
    }
}
