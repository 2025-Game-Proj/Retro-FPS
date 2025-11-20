using UnityEngine;

// 디버그용: 추후 삭제
public class AmmoCounterDebug : MonoBehaviour, IAmmoProvider
{
    [SerializeField] private int max = 30;
    [SerializeField] private int current = 30;
    public int Current => current;
    public int Max => max;

    public event System.Action<int, int> OnAmmoChanged;

    [Header("Test Inputs")]
    // 좌클릭 발사, R 장전
    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Awake()
    {
        current = Mathf.Clamp(current, 0, max);
        OnAmmoChanged?.Invoke(current, max);
    }

    private void Update()
    {
        // 발사: 마우스 왼쪽 버튼
        if (Input.GetMouseButtonDown(0) && current > 0)
        {
            current--;
            OnAmmoChanged?.Invoke(current, max);
        }

        // 장전
        if (Input.GetKeyDown(reloadKey))
        {
            current = max;
            OnAmmoChanged?.Invoke(current, max);
        }
    }

    private void OnEnable()
    {
        current = Mathf.Clamp(current, 0, max);
        OnAmmoChanged?.Invoke(current, max); // 초기 푸시
    }
}
