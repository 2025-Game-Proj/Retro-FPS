using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private Animator animator;  // Add this

    // Optional: Set different speeds for idle/aggro states
    [SerializeField] private float walkSpeed = 2f;
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
        if (enemyAwareness.isAggro)
        {
            // Chase the player at run speed
            enemyNavMeshAgent.speed = runSpeed;
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            // Stop or patrol at walk speed
            enemyNavMeshAgent.speed = walkSpeed;
            enemyNavMeshAgent.SetDestination(transform.position);
        }

        // Update animator with current velocity
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        // Get the actual speed of the NavMeshAgent
        float currentSpeed = enemyNavMeshAgent.velocity.magnitude;

        // Pass the speed to the animator
        animator.SetFloat("Speed", currentSpeed);
    }
}