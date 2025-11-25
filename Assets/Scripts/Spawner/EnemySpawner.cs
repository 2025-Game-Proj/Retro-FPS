using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] spawnPoints;
    private int maxEnemyCount = 35;
    private int curEnemyCount = 0;
    private int bossAppear = 30;
    private float spawnPeriod = 30f;
    private int killedEnemy = 0;
    public EnemyHealth enemyPrefab;
    private WaitForSeconds wait;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        wait = new WaitForSeconds(spawnPeriod);
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            if (curEnemyCount < maxEnemyCount)
            {
                foreach (GameObject obj in spawnPoints)
                {
                    if(curEnemyCount >= maxEnemyCount)
                    {
                        break;
                    }
                    float spread = 3f; // increase this for even more spacing

                    Vector3 offset = new Vector3(
                        Random.Range(-spread, spread),
                        0,
                        Random.Range(-spread, spread)
                    );

                    EnemyHealth enemy = Instantiate(enemyPrefab, obj.transform.position + offset, obj.transform.rotation);
                    enemy.SetMaxHealth(18);
                    enemy.onDeath += () =>
                    {
                        curEnemyCount--;
                        killedEnemy++;
                        if (killedEnemy == bossAppear)
                        {
                            EnemyHealth boss = Instantiate(enemyPrefab, transform.position, transform.rotation);
                            boss.transform.localScale *= 2;
                            boss.SetMaxHealth(300);
                            boss.onDeath += OnBossDeath;
                        }
                    };

                    curEnemyCount ++;
                }
                //curEnemyCount += spawnPoints.Length;
            }
            yield return wait;
        }
    }
    private void OnBossDeath()
    {
        // Debug.Log("Clear");
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StopTimer();
        }
        SceneManager.LoadScene("GameClear");
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
