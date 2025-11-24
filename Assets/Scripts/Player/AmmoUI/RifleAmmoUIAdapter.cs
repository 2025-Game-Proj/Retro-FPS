using UnityEngine;

[RequireComponent(typeof(Rifle))]
public class RifleAmmoUIAdapter : MonoBehaviour
{
    [Header("UI View")]
    [SerializeField] private AmmoHUDView ammoHUD; // Canvas �� AmmoHUDView �巡��

    private Rifle rifle;

    // ���������� ǥ���� ��(��ȭ�� ���� ���� UI ����)
    private int lastMagazine = int.MinValue;
    private int lastTotalAmmo = int.MinValue;

    private void Awake()
    {
        rifle = GetComponent<Rifle>();
    }

    private void Start()
    {
        // ���� �� �� �� ���� ����
        ForceUpdateUI();
    }

    private void Update()
    {
        if (!rifle || !ammoHUD) return;

        int curMag = rifle.curMagazine;
        int totalAmmo = rifle.ammo;

        // ���� �ٲ� ��쿡�� UI ����
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