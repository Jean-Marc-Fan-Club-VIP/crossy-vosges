using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> ScoreUpdated;

    public static void OnScoreUpdated(int newScore)
    {
        ScoreUpdated?.Invoke(newScore);
    }
}