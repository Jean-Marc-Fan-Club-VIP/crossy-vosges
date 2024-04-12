using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> ScoreUpdated;
    public static event UnityAction<float> TimerUpdated;
    public static event UnityAction TimerStarted;
    public static event UnityAction TimerStopped;

    public static event UnityAction GameOver;

    public static void OnGameOver()
    {
        GameOver?.Invoke();
        TimerStopped?.Invoke();
    }

    public static void OnScoreUpdated(int newScore)
    {
        ScoreUpdated?.Invoke(newScore);
    }

    public static void OnTimerUpdated(float time)
    {
        TimerUpdated?.Invoke(time);
    }

    public static void OnTimerStarted()
    {
        TimerStarted?.Invoke();
    }

    public static void OnTimerStopped()
    {
        TimerStopped?.Invoke();
    }
}