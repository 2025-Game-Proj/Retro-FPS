using UnityEngine;

// UI와 체력 시스템 연결
public class HealthBarBinder : MonoBehaviour
{
    [SerializeField] private HealthBarView view;
    private IHealthProvider provider;

    public void Bind(IHealthProvider p)
    {
        if (provider != null)
            provider.OnHealthChanged -= OnHealthChanged;

        provider = p;

        if (provider != null)
        {
            provider.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(provider.Current, provider.Max); // 초기값 반영
        }
    }

    private void OnHealthChanged(float current, float max)
    {
        view.SetValue(current, max);
    }
}
