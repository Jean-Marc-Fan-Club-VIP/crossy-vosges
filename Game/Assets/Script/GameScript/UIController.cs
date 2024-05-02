using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TMP_Text score;
    
    private TMP_Text timer;
    
    private void Awake()
    {
        score = transform.Find("HLayout/Panel/Score").GetComponent<TMP_Text>();
        timer = transform.Find("HLayout/Panel/Time").GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
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