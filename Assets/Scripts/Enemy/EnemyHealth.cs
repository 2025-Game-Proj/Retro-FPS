using UnityEngine;

public class EnemyHealth : Health
{
    public delegate void DeathCallback();
    public event DeathCallback onDeath;
    public GameObject coinPrefab;
    public override void OnDeath()
    {
        if(onDeath != null)
        {
            onDeath.Invoke();
            Instantiate(coinPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
