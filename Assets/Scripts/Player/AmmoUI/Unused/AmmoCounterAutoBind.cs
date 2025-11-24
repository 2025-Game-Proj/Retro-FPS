using UnityEngine;

[RequireComponent(typeof(AmmoCounterBinder))]
public class AmmoCounterAutoBind : MonoBehaviour
{
    void Start()
    {
        var binder = GetComponent<AmmoCounterBinder>();

        // 1) 같은 오브젝트에서 먼저 찾기
        var provider = GetComponent<IAmmoProvider>();

        // 2) 없으면 씬 전체에서 첫 번째 것 찾기 (2023+ API)
        if (provider == null)
        {
#if UNITY_2023_1_OR_NEWER
            // 비활성 오브젝트까지 포함하려면 인자 사용
            provider = Object.FindFirstObjectByType<MonoBehaviour>(FindObjectsInactive.Include) as IAmmoProvider
                    ?? Object.FindAnyObjectByType<MonoBehaviour>(FindObjectsInactive.Include) as IAmmoProvider;
#else
            // 구버전 호환
            provider = Object.FindObjectOfType<MonoBehaviour>(true) as IAmmoProvider;
#endif
        }

        if (provider != null) binder.Bind(provider);
        else Debug.LogWarning("[AutoBind] IAmmoProvider not found in scene.");
    }
}
