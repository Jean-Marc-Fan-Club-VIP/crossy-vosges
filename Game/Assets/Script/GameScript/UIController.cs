using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void OnEnable()
    {
        EventManager.ScoreUpdated += EventManagerOnScoreUpdated;
    }

    private void OnDisable()
    {
        EventManager.ScoreUpdated -= EventManagerOnScoreUpdated;
    }

    private void EventManagerOnScoreUpdated(int value)
    {
        score.text = $"Score : {value}";
    }
}