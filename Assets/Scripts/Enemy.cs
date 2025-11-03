using UnityEngine;

// 적 캐릭터의 기본 동작을 담당하는 클래스
public class Enemy : MonoBehaviour
{

    public EnemyManager enemyManager;   // 적 관리자 참조 (적이 제거될 때 리스트에서도 제거하기 위함)
    private float enemyHealth = 2f;     // 체력 (초기값: 2)


    void Start()
    {

    }


    void Update()
    {

        if (enemyHealth <= 0)
        {
            // 적 관리자의 리스트에서 자신을 제거
            enemyManager.RemoveEnemy(this);

            // 게임 오브젝트 파괴 (씬에서 적 제거)
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }
}