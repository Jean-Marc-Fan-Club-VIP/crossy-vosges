using System;
using Script.GameScript;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TMP_Text bestScore;
    private GameStatsController gameStatsController;
    private TMP_Text score;
    private TMP_Text timer;
    
    private void Awake()
    {
        gameStatsController = new GameStatsController();
        bestScore = transform.Find("HLayout/BestScore").GetComponent<TMP_Text>();
        score = transform.Find("HLayout/Panel/Score").GetComponent<TMP_Text>();
        timer = transform.Find("HLayout/Panel/Time").GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        bestScore.text = $"Best: {gameStatsController.GetBestScore(LevelSelector.LevelGame())}";
        EventManager.StartTimer();
    }
    
    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
        EventManager.TimerUpdated += EventManagerOnTimerUpdated;
        EventManager.GameOver += EventManagerOnGameOver;
    }
    
    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
        EventManager.TimerUpdated -= EventManagerOnTimerUpdated;
        EventManager.GameOver -= EventManagerOnGameOver;
    }
    
    private void EventManagerOnGameOver()
    {
        gameObject.SetActive(false);
    }
    
    private void EventManagerOnTimerUpdated(float value)
    {
        var timeSpan = TimeSpan.FromSeconds(value);
        timer.text = timeSpan.ToString(@"mm\:ss");
    }
    
    private void EventManagerOnScoreUpdated(int value)
    {
        score.text = $"Score : {value}";
    }
}