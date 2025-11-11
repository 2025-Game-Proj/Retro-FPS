using UnityEngine;

public class HealItem : Item
{
    private int heal = 50;
    public void SetHeal(int amount)
    {
        heal = amount;
    }
    public override void OnObtained(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health)
        {
            health.Heal(heal);
        }
    }
}
