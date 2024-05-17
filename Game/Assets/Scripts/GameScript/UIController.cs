using System;
using Script.GameScript;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private int bestScore;
    private TMP_Text bestScoreTMP;
    private TMP_Text coinsTMP;
    private GameStatsController gameStatsController;
    private TMP_Text scoreTMP;
    private TMP_Text timerTMP;
    
    private void Awake()
    {
        gameStatsController = new GameStatsController();
        bestScoreTMP = transform.Find("BestScore").GetComponent<TMP_Text>();
        scoreTMP = transform.Find("Score").GetComponent<TMP_Text>();
        timerTMP = transform.Find("Time").GetComponent<TMP_Text>();
        coinsTMP = transform.Find("CoinsLayout/Coins").GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        bestScore = gameStatsController.GetBestScore(LevelSelector.LevelGame());
        bestScoreTMP.text = $"{bestScore}";
        EventManager.StartTimer();
    }
    
    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
        EventManager.TimerUpdated += EventManagerOnTimerUpdated;
        EventManager.GameOver += EventManagerOnGameOver;
        EventManager.CoinsUpdated += EventManagerOnCoinsUpdated;
    }
    
    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
        EventManager.TimerUpdated -= EventManagerOnTimerUpdated;
        EventManager.GameOver -= EventManagerOnGameOver;
        EventManager.CoinsUpdated -= EventManagerOnCoinsUpdated;
    }
    
    private void EventManagerOnCoinsUpdated(int coins)
    {
        coinsTMP.text = coins.ToString();
    }
    
    private void EventManagerOnGameOver()
    {
        gameObject.SetActive(false);
    }
    
    private void EventManagerOnTimerUpdated(float value)
    {
        var timeSpan = TimeSpan.FromSeconds(value);
        timerTMP.text = timeSpan.ToString(@"mm\:ss");
    }
    
    private void EventManagerOnScoreUpdated(int value)
    {
        scoreTMP.text = $"{value}";
        if (value > bestScore)
        {
            bestScoreTMP.gameObject.SetActive(false);
        }
    }
}