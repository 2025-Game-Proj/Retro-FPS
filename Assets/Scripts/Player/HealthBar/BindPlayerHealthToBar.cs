using UnityEngine;

public class BindPlayerHealthToBar : MonoBehaviour
{
    [SerializeField] private HealthBarBinder binder;
    [SerializeField] private MonoBehaviour providerBehaviour; // IHealthProvider 구현체 (어댑터) 드래그

    private void Start()
    {
        if (!binder)
            binder = GetComponent<HealthBarBinder>();

        if (!binder)
        {
            Debug.LogError("[BindPlayerHealthToBar] HealthBarBinder가 연결되어 있지 않습니다.");
            return;
        }

        if (providerBehaviour is IHealthProvider provider)
        {
            binder.Bind(provider);
        }
        else
        {
            Debug.LogError("[BindPlayerHealthToBar] providerBehaviour가 IHealthProvider를 구현하지 않습니다.");
        }
    }
}
