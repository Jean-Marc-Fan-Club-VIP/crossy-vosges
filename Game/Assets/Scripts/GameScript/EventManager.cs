using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> ScoreUpdated;
    public static event UnityAction<float> TimerUpdated;
    public static event UnityAction TimerStarted;
    public static event UnityAction TimerStopped;
    public static event UnityAction<int> CoinsUpdated;
    
    public static event UnityAction GameOver;
    
    public static void OnGameOver()
    {
        GameOver?.Invoke();
        TimerStopped?.Invoke();
    }
    
    public static void UpdateCoins(int value)
    {
        CoinsUpdated?.Invoke(value);
    }
    
    public static void UpdateScore(int newScore)
    {
        ScoreUpdated?.Invoke(newScore);
    }
    
    public static void UpdateTime(float time)
    {
        TimerUpdated?.Invoke(time);
    }
    
    public static void StartTimer()
    {
        TimerStarted?.Invoke();
    }
    
    public static void StopTimer()
    {
        TimerStopped?.Invoke();
    }
}