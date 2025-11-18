/*using UnityEngine;

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
}*/



using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    private float enemyHealth = 2f;
    private Animator animator;  // Add this
    private bool isDead = false;  // Prevent multiple death calls

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Optional: Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");  // Create this trigger in Animator
        }

        // Disable movement
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;

        // Remove from manager
        enemyManager.RemoveEnemy(this);

        // Destroy after animation (adjust delay to match animation length)
        Destroy(gameObject, 2f);  // 2 seconds delay
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }
}