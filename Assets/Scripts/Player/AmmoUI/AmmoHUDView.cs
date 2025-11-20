using UnityEngine;
using TMPro;

public class AmmoHUDView : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TMP_Text magazineText; // 좌측: 현재 탄창
    [SerializeField] private TMP_Text totalText;    // 우측: 전체 보유 탄약
    [SerializeField] private TMP_Text slashText;    // 가운데: "/"

    [Header("Low ammo color (optional)")]
    [SerializeField] private int lowAmmoThreshold = 5;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color lowAmmoColor = new Color(1f, 0.35f, 0.35f);

    public void SetAmmo(int currentMagazine, int totalAmmo)
    {
        if (magazineText != null)
            magazineText.text = Mathf.Max(0, currentMagazine).ToString();

        if (totalText != null)
            totalText.text = Mathf.Max(0, totalAmmo).ToString();

        // 저탄약 시 색상 경고 (탄창 기준)
        if (magazineText != null)
        {
            bool low = currentMagazine > 0 && currentMagazine <= lowAmmoThreshold;
            magazineText.color = low ? lowAmmoColor : normalColor;
        }

        // 슬래시는 한 번만 세팅해도 되지만, 혹시 비어 있으면 채워줌
        if (slashText != null && string.IsNullOrEmpty(slashText.text))
            slashText.text = "/";
    }

    // 인스펙터에서 자동으로 자식 TMP 찾기 (실수 방지용)
    private void OnValidate()
    {
        if (!magazineText) magazineText = transform.Find("Text_Magazine")?.GetComponent<TMP_Text>();
        if (!slashText) slashText = transform.Find("Text_Slash")?.GetComponent<TMP_Text>();
        if (!totalText) totalText = transform.Find("Text_Total")?.GetComponent<TMP_Text>();
    }
}
