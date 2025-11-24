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
            Instantiate(coinPrefab, transform.position + new Vector3(0,0.4f,0), transform.rotation);
        }
        Destroy(gameObject);
    }
}
