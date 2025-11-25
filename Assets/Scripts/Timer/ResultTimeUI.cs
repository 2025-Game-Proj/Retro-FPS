// ResultTimeUI.cs
using UnityEngine;
using TMPro;

public class ResultTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        if (TimerManager.Instance != null)
        {
            float t = TimerManager.Instance.GetElapsedTime();
            // 보기 좋게 포맷팅 (분:초.밀리초)
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            int millis = Mathf.FloorToInt((t * 1000f) % 1000f);

            timeText.text = $"{minutes:00}:{seconds:00}.{millis:000}";
        }
        else
        {
            timeText.text = "시간 정보 없음";
        }
    }
}
