using UnityEngine;
using System;

[RequireComponent(typeof(Health))]
public class HealthToProviderAdapter : MonoBehaviour, IHealthProvider
{
    [SerializeField] private Health target;  // PlayerHealth 포함 Health 계열 참조

    public float Current => target ? target.currentHealth : 0f;
    public float Max => target ? target.maxHealth : 1f;

    public event Action<float, float> OnHealthChanged;

    private int _lastCurrent;
    private int _lastMax;
    private bool _initialized;

    private void Awake()
    {
        if (!target)
            target = GetComponent<Health>(); // 같은 오브젝트에 PlayerHealth가 붙어있다면 자동
    }

    private void OnEnable()
    {
        TryInitAndNotify();
    }

    private void Start()
    {
        // Start에서 한 번 더: Health.Start() 이후 값이 확정된 상태를 반영
        TryInitAndNotify();
    }

    private void Update()
    {
        if (!target) return;

        int cur = target.currentHealth;
        int max = target.maxHealth;

        if (!_initialized || cur != _lastCurrent || max != _lastMax)
        {
            _initialized = true;
            _lastCurrent = cur;
            _lastMax = max;
            OnHealthChanged?.Invoke(cur, max);
        }
    }

    private void TryInitAndNotify()
    {
        if (!target) return;
        _lastCurrent = target.currentHealth;
        _lastMax = target.maxHealth;
        _initialized = true;
        OnHealthChanged?.Invoke(_lastCurrent, _lastMax);
    }
}
