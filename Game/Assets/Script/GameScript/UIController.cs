using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private TMP_Text _timer;

    private void Start()
    {
        EventManager.OnTimerStarted();
    }

    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
        EventManager.TimerUpdated += EventManagerOnTimerUpdated;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
        EventManager.TimerUpdated -= EventManagerOnTimerUpdated;
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