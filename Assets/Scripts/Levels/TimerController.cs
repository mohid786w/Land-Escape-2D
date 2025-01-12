using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    private static float timeElapsed;
    private bool timerRunning;

    private void Start()
    {
        timerRunning = true;
        timeElapsed = 0f;
    }

    private void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            timerText.text = FormatTime(timeElapsed);
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
        PlayerPrefs.SetFloat("CompletionTime", timeElapsed);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
