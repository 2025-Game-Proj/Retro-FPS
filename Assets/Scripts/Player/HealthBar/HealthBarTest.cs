using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    [SerializeField] private HealthBarView view;

    private void Start()
    {
        if (view == null)
            view = GetComponent<HealthBarView>();

        if (view != null)
        {
            // 시작할 때 체력 75/100으로 표시해보기
            view.SetValue(75, 100);
        }
        else
        {
            Debug.LogWarning("[HealthBarTest] HealthBarView가 연결되어 있지 않습니다.");
        }
    }
}
