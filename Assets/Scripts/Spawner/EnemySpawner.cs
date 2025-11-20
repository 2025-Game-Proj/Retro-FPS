using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] spawnPoints;
    private int maxEnemyCount = 20;
    private int curEnemyCount = 0;
    private int bossAppear = 1;
    public Transform bossPosition;
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
            if(curEnemyCount < maxEnemyCount)
            {
                foreach(GameObject obj in spawnPoints)
                {
                    EnemyHealth enemy = Instantiate(enemyPrefab, obj.transform.position, obj.transform.rotation);
                    enemy.SetMaxHealth(50);
                    enemy.onDeath += () =>
                    {
                        curEnemyCount--;
                        killedEnemy++;
                        if(killedEnemy == bossAppear)
                        {
                            EnemyHealth boss = Instantiate(enemyPrefab, bossPosition.position, bossPosition.rotation);
                            boss.transform.localScale *= 2;
                            boss.SetMaxHealth(200);
                            boss.onDeath += OnBossDeath;
                        }
                    };
                }
                curEnemyCount += spawnPoints.Length;
            }
            yield return wait;
        }
    }
    private void OnBossDeath()
    {
        Debug.Log("Clear");
    }
    private void OnDestroy() {
        StopAllCoroutines();
    }
    
}
