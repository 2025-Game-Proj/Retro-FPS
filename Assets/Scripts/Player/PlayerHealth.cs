using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public override void OnDeath()
    {
        SceneManager.LoadScene("GameOver");
    }
}
