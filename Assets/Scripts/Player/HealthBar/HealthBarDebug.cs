using UnityEngine;

// 체력바 디버그용 
// 추후 삭제 필요
public class HealthBarDebug : MonoBehaviour
{
    [SerializeField] private HealthBarView view;
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;
    [SerializeField] private float tick = 25f; // 키 입력 당 증감량

    void Update()
    {
        // 임시 조작: J=피해, K=회복
        if (Input.GetKeyDown(KeyCode.J)) currentHP -= tick;
        if (Input.GetKeyDown(KeyCode.K)) currentHP += tick;

        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        view.SetValue(currentHP, maxHP);
    }
}
