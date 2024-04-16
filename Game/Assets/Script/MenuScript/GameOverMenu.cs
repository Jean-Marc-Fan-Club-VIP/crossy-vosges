using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;
    [SerializeField] public TMP_Text _score;
    [SerializeField] private TMP_Text _timer;
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
        EventManager.TimerUpdated += EventManagerOnTimerUpdated;
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
    }

    private void OnDisable()
    {
        EventManager.GameOver -= EventManagerOnGameOver;
        EventManager.TimerUpdated -= EventManagerOnTimerUpdated;
    }

    private void EventManagerOnScoreUpdated(int value)
    {
        _score.text = $"{value}";
    }

    private void EventManagerOnTimerUpdated(float value)
    {
        var timeSpan = TimeSpan.FromSeconds(value);
        _timer.text = timeSpan.ToString(@"mm\:ss");
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