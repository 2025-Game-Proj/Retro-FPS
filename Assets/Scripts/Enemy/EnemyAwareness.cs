using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{

    public bool isAggro;
    public float awarenessDistance = 8;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMove>().transform;
    }


    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist < awarenessDistance)
        {
            isAggro = true;
        }
    }

}
