using UnityEngine;

public class CoinItem : Item
{
    public override void OnObtained(GameObject player)
    {
        Rifle rifle = player.GetComponentInChildren<Rifle>();
        if (rifle)
        {
            rifle.AddCoin();
        }
    }

}
