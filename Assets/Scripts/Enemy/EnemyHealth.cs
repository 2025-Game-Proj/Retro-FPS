using UnityEngine;

public class EnemyHealth : Health
{
    public delegate void DeathCallback();
    public event DeathCallback onDeath;
    public override void OnDeath()
    {
        if(onDeath != null)
        {
            onDeath.Invoke();
        }
        Destroy(gameObject);
    }
}
