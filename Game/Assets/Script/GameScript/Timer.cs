using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isRunning;
    private float time;

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        time += Time.deltaTime;
        EventManager.UpdateTime(time);
    }

    private void OnEnable()
    {
        EventManager.TimerStarted += EventManagerOnTimerStarted;
        EventManager.TimerStopped += EventManagerOnTimerStopped;
    }

    private void OnDisable()
    {
        EventManager.TimerStarted -= EventManagerOnTimerStarted;
        EventManager.TimerStopped -= EventManagerOnTimerStopped;
    }

    private void EventManagerOnTimerStopped()
    {
        isRunning = false;
    }

    private void EventManagerOnTimerStarted()
    {
        isRunning = true;
    }
}