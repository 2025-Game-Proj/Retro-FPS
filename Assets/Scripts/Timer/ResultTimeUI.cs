using UnityEngine;
using TMPro;

public class ResultTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    private void Start()
    {
        float nowTime = TimerManager.Instance.GetElapsedTime();
        float bestTime = BestTimeManager.GetBestTime();

        currentTimeText.text = $"Current Record: {FormatTime(nowTime)}";

        if (bestTime == Mathf.Infinity)
            bestTimeText.text = $"Best Record: None";
        else
            bestTimeText.text = $"Best Record: {FormatTime(bestTime)}";
    }

    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int millis = Mathf.FloorToInt((t * 1000f) % 1000f);
        return $"{minutes:00}:{seconds:00}.{millis:000}";
    }
}
