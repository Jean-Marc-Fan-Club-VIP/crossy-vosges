using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private TMP_Text _timer;
    [SerializeField] private GameObject _stats;

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
        _stats.SetActive(false);
    }

    private void EventManagerOnTimerUpdated(float value)
    {
        var timeSpan = TimeSpan.FromSeconds(value);
        _timer.text = timeSpan.ToString(@"mm\:ss");
    }

    private void EventManagerOnScoreUpdated(int value)
    {
        score.text = $"Score : {value}";
    }
}