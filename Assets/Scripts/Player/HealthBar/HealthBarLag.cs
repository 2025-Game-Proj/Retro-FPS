// HealthBarView와 같은 오브젝트에 붙여도 되고, 안에 통합해도 됩니다.
using UnityEngine;
using UnityEngine.UI;

// 대미지 잔상바, 실제 Fill은 즉시 줄고, DamageLag는 약간 늦게 따라감
public class HealthBarLag : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image damageLag;     // DamageLag 이미지
    [SerializeField] private float lagSpeed = 2f; // 초당 따라가는 속도

    public void Set01(float t) // t = current/max (0~1)
    {
        if (fill) fill.fillAmount = t;
        if (damageLag)
        {
            // 줄어들 때만 지연: damageLag가 fill보다 크면 서서히 감소
            if (damageLag.fillAmount > t)
                damageLag.fillAmount = Mathf.MoveTowards(damageLag.fillAmount, t, lagSpeed * Time.unscaledDeltaTime);
            else
                damageLag.fillAmount = t; // 회복은 즉시(원하면 반대로)
        }
    }
}
