using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 기본적인 체력바 메커니즘
public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image fill;         // Filled Image
    [SerializeField] private TMP_Text label;     // "75 / 100" 등
    [Range(0, 1f)][SerializeField] private float lowHPThreshold = 0.2f;
    [SerializeField] private CanvasGroup lowHPFlash; // 선택(없어도 됨)

    public void SetValue(float current, float max)
    {
        max = Mathf.Max(1f, max);
        current = Mathf.Clamp(current, 0f, max);

        if (fill) fill.fillAmount = current / max;
        if (label) label.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";

        // 낮은 체력 경고 (선택)
        if (lowHPFlash)
        {
            bool low = (current / max) <= lowHPThreshold;
            lowHPFlash.alpha = low ? Mathf.Abs(Mathf.Sin(Time.unscaledTime * 6f)) : 0f;
        }
    }
}
