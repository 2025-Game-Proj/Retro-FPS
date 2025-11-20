using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.2f;
    private float lastAttackTime = -999f;

    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private Animator animator;  // Add this

    // Optional: Set different speeds for idle/aggro states
    //[SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = FindFirstObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();  // Get the Animator component
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (enemyAwareness.isAggro)
        {
            // If close enough → ATTACK
            if (dist <= attackRange)
            {
                enemyNavMeshAgent.SetDestination(transform.position); // Stop movement
                TryAttack();
            }
            else
            {
                // CHASE
                enemyNavMeshAgent.speed = runSpeed;
                enemyNavMeshAgent.SetDestination(playerTransform.position);
            }
        }
        else
        {
            // Not aggro → idle
            enemyNavMeshAgent.speed = runSpeed;
            enemyNavMeshAgent.SetDestination(transform.position);
        }

        UpdateAnimator();
    }


    public void DealDamage()
    {
        // Safety check
        if (playerTransform == null) return;

        // Call the player's health script (change PlayerHealth to your script name)
        playerTransform.GetComponent<PlayerHealth>().ApplyDamage(10);
    }



    private void TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        // Trigger attack animation
        animator.SetTrigger("Attack");

        // Rotate toward the player (optional but usually needed)
        Vector3 dir = playerTransform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);

        // Damage is usually done through animation events, but you can place it here too
        // player.TakeDamage();
    }


    private void UpdateAnimator()
    {
        // Get the actual speed of the NavMeshAgent
        float currentSpeed = enemyNavMeshAgent.velocity.magnitude;

        // Pass the speed to the animator
        animator.SetFloat("Speed", currentSpeed);
    }
}