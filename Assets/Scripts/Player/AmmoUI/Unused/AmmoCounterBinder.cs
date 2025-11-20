// AmmoCounterBinder.cs
using UnityEngine;

public class AmmoCounterBinder : MonoBehaviour
{
    [SerializeField] private AmmoCounterView view;
    private IAmmoProvider provider;

    public void Bind(IAmmoProvider p)
    {
        Debug.Log($"[Binder] Bind called. binder={name}, view={(view ? view.name : "null")}, provider={(p == null ? "null" : p.ToString())}");

        if (provider != null)
        {
            provider.OnAmmoChanged -= OnAmmoChanged;
            Debug.Log("[Binder] Unsubscribed previous provider");
        }

        provider = p;

        if (provider != null)
        {
            provider.OnAmmoChanged += OnAmmoChanged;
            Debug.Log($"[Binder] Subscribed to provider instanceID={(provider as Object)?.GetInstanceID()}");
            OnAmmoChanged(provider.Current, provider.Max); // 초기 반영
        }
        else
        {
            Debug.LogWarning("[Binder] provider is null - not bound");
        }
    }

    private void OnAmmoChanged(int cur, int max)
    {
        Debug.Log($"[Binder] AmmoChanged: {cur}/{max}");
        view.SetValue(cur, max);
    }

    private void OnEnable()
    {
        // 이미 바인딩된 상태면 재반영 (옵션)
        if (provider != null) OnAmmoChanged(provider.Current, provider.Max);
    }
}
