using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private List<float> timers = new List<float>();
    private float wholeTime;
    private bool timerIsRunning = false;
    private float startTimeLapsed = 0;
    private bool lapsedTimerStarted = false;

    public void StartTimer()
    {
        startTimeLapsed = 0;
        wholeTime = 0;
        timers.Clear();
        
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void StartLapseTimer()
    {
        startTimeLapsed = Time.time;
        lapsedTimerStarted = true;
    }

    public void EndLapseTimer()
    {
        if (!lapsedTimerStarted) return;
        lapsedTimerStarted = false;
        Debug.Log($"Time : {Time.time - startTimeLapsed}");
        timers.Add(Time.time - startTimeLapsed);
        startTimeLapsed = 0;
    }

    private void Update()
    {
        if (!timerIsRunning) return;
        
        wholeTime += Time.deltaTime;
    }

    public string GetTimeString(out int listCount)
    {
        listCount = timers.Count;
        string timersString = string.Join(",", timers.Select(f => f.ToString("F2", CultureInfo.InvariantCulture)));
        timersString+=","+wholeTime.ToString("F2", CultureInfo.InvariantCulture);
        return timersString;
    }
}
