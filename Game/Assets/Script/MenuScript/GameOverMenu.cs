using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;
    private AudioControler audioController;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioControler>();
    }

    private void Start()
    {
        gameOverMenuUi.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.GameOver += EventManagerOnGameOver;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EventManagerOnGameOver;
    }

    private void EventManagerOnGameOver()
    {
        if (audioController)
        {
            audioController.PauseBackgroundMusic();
        }

        gameOverMenuUi.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReplayGame()
    {
        if (audioController != null)
        {
            audioController.ResumeBackgroundMusic();
        }

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
}