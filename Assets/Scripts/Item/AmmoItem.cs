using UnityEngine;

public class AmmoItem : Item
{
    private int ammo = 50;
    public void SetAmmo(int amount)
    {
        ammo = amount;
    }
    public override void OnObtained(GameObject player)
    {
        Rifle rifle = player.GetComponentInChildren<Rifle>();
        if (rifle)
        {
            rifle.AddAmmo(ammo);
        }
    }
}
