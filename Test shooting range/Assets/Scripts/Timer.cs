using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private bool timerStarted;
    private float _timer;

    public void StartTimer()
    {
        timerStarted = true;
    }

    public float StopTimer()
    {
        timerStarted = false;
        return _timer;
    }

    //Действие таймера
    private void Update()
    {
        if (!timerStarted)
            return;
        _timer += Time.deltaTime;
        timerText.text = Mathf.Round(_timer).ToString();
    }

    public int GetTime()
    {
        return (int) Mathf.Round(_timer);
    }
}