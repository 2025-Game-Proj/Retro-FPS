using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;


    void Start()
    {
        currentHealth = maxHealth;
    }
    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public abstract void OnDeath();
    public void Heal(int heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }
    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
    }
}
