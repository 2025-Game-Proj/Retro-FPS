using UnityEngine;
using TMPro;

public class AmmoCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text count;      // 큼직한 중앙 숫자
    [SerializeField] private int lowAmmoThreshold = 5;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color lowAmmoColor = new Color(1f, 0.35f, 0.35f); // 살짝 붉은색

    private void Start()
    {
        if (count && string.IsNullOrEmpty(count.text))
            count.text = "--";   // 최소한 글자 찍히는지 확인
    }

    // current: 현재 탄수 (예: 탄창에 남은 탄)
    // max: 최대 탄창 용량 (예: 30)
    public void SetValue(int current, int max)
    {
        if (!count) return;

        current = Mathf.Max(0, current);
        max = Mathf.Max(1, max);

        Debug.Log($"AmmoView SetValue -> {current}/{max}");
        count.text = current.ToString(); // 요구사항: 박스 안에 큰 숫자 1개

        // 저탄약 시 색상 경고
        count.color = (current <= Mathf.Min(lowAmmoThreshold, max - 1)) ? lowAmmoColor : normalColor;
    }

    // 호환용 (체력바와 이름 맞추고 싶으면)
    //public void SetValues(float current, float max) => SetValue(Mathf.RoundToInt(current), Mathf.RoundToInt(max));
}
