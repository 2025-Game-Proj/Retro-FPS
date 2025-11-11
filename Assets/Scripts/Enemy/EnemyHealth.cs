using UnityEngine;

public class EnemyHealth : Health
{
    public override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
