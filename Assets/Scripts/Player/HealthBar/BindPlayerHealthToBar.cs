using UnityEngine;

public class BindPlayerHealthToBar : MonoBehaviour
{
    [SerializeField] private HealthBarBinder binder;
    [SerializeField] private MonoBehaviour providerBehaviour; // IHealthProvider ����ü (�����) �巡��

    private void Start()
    {
        if (!binder)
            binder = GetComponent<HealthBarBinder>();

        if (!binder)
        {
            Debug.LogError("[BindPlayerHealthToBar] HealthBarBinder�� ����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        if (providerBehaviour is IHealthProvider provider)
        {
            binder.Bind(provider);
        }
        else
        {
            Debug.LogError("[BindPlayerHealthToBar] providerBehaviour�� IHealthProvider�� �������� �ʽ��ϴ�.");
        }
    }
}