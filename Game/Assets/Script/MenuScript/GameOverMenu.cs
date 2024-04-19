using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameOverMenu : MonoBehaviour
{
    private const string StatsPath = "stats.json";
    public GameObject gameOverMenuUi;

    [FormerlySerializedAs("_score")] [SerializeField]
    public TMP_Text scoreText;

    [FormerlySerializedAs("_timer")] [SerializeField]
    private TMP_Text timerText;

    private AudioControler audioController;
    private IDataService dataService;
    private GameStats stats;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioControler>();
        dataService = new JsonDataService();
    }

    private void Start()
    {
        gameOverMenuUi.SetActive(false);
        stats = new GameStats();
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
        stats.Score = value;
        scoreText.text = $"{stats.Score}";
    }

    private void EventManagerOnTimerUpdated(float value)
    {
        stats.Time = TimeSpan.FromSeconds(value);
        timerText.text = stats.Time.ToString(@"mm\:ss");
    }

    private void EventManagerOnGameOver()
    {
        if (audioController)
        {
            audioController.PauseBackgroundMusic();
        }

        gameOverMenuUi.SetActive(true);
        Time.timeScale = 0f;
        SaveStats();
    }

    private void SaveStats()
    {
        IEnumerable<GameStats> previousStats;
        try
        {
            previousStats = dataService.LoadEntity<IEnumerable<GameStats>>(StatsPath);
        }
        catch (Exception e)
        {
            Debug.Log($"{e}. Creating a new stat file");
            previousStats = new List<GameStats>();
        }

        stats.Name = OptionsMenu.PlayerName;
        previousStats = previousStats.Append(stats);
        dataService.SaveEntity(StatsPath, previousStats);
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