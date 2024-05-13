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
        bestScoreTMP = transform.Find("HLayout/BestScore").GetComponent<TMP_Text>();
        scoreTMP = transform.Find("HLayout/Panel/Score").GetComponent<TMP_Text>();
        timerTMP = transform.Find("HLayout/Panel/Time").GetComponent<TMP_Text>();
        coinsTMP = transform.Find("HLayout/Panel/Coins").GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        bestScore = gameStatsController.GetBestScore(LevelSelector.LevelGame());
        bestScoreTMP.text = $"Best: {bestScore}";
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
        coinsTMP.text = $"Coins: {coins}";
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
        scoreTMP.text = $"Score : {value}";
        if (value > bestScore)
        {
            bestScoreTMP.text = $"Best: {value}";
        }
    }
}