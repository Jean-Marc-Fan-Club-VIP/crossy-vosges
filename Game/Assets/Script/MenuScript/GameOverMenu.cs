using System;
using System.Linq;
using Script.GameScript;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUi;
    
    [FormerlySerializedAs("_score")] [SerializeField]
    public TMP_Text scoreText;
    
    [FormerlySerializedAs("_timer")] [SerializeField]
    private TMP_Text timerText;
    
    [SerializeField] private TMP_Text rankText;
    private AudioControler audioController;
    
    private GameStatsController gameStatsController;
    private RunStats stats;
    
    private void Awake()
    {
        audioController = FindObjectOfType<AudioControler>();
        gameStatsController = new GameStatsController();
    }
    
    private void Start()
    {
        gameOverMenuUi.SetActive(false);
        stats = new RunStats
        {
            Level = LevelSelector.LevelGame()
        };
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
        var previousStats = gameStatsController.GetGameStats();
        stats.Name = OptionsMenu.PlayerName;
        previousStats.Add(stats);
        var rank = previousStats.Where(rs => rs.Level == LevelSelector.LevelGame()).OrderByDescending(rs => rs.Score)
            .ToList()
            .FindIndex(rs => rs.Score <= stats.Score) + 1;
        rankText.SetText($"Your rank: #{rank}");
        GameStatsController.SaveGameStats(previousStats);
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