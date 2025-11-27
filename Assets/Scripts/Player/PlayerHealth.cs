using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private string deathSceneName = "GameOver";
    public override void OnDeath()
    {
        SceneManager.LoadScene(deathSceneName);
    }
}
