using UnityEngine;

[RequireComponent(typeof(Rifle))]
public class RifleAmmoUIAdapter : MonoBehaviour
{
    [Header("UI View")]
    [SerializeField] private AmmoHUDView ammoHUD; // Canvas 쪽 AmmoHUDView 드래그

    private Rifle rifle;

    // 마지막으로 표시한 값(변화가 있을 때만 UI 갱신)
    private int lastMagazine = int.MinValue;
    private int lastTotalAmmo = int.MinValue;

    private void Awake()
    {
        rifle = GetComponent<Rifle>();
    }

    private void Start()
    {
        // 시작 시 한 번 강제 갱신
        ForceUpdateUI();
    }

    private void Update()
    {
        if (!rifle || !ammoHUD) return;

        int curMag = rifle.curMagazine;
        int totalAmmo = rifle.ammo;

        // 값이 바뀐 경우에만 UI 갱신
        if (curMag != lastMagazine || totalAmmo != lastTotalAmmo)
        {
            lastMagazine = curMag;
            lastTotalAmmo = totalAmmo;
            ammoHUD.SetAmmo(curMag, totalAmmo);
        }
    }

    private void ForceUpdateUI()
    {
        if (!rifle || !ammoHUD) return;

        lastMagazine = rifle.curMagazine;
        lastTotalAmmo = rifle.ammo;
        ammoHUD.SetAmmo(lastMagazine, lastTotalAmmo);
    }
}
