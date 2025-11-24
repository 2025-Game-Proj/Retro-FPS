using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public HealItem healItem;
    public AmmoItem ammoItem;

    public int maxItemCount = 15;   // 🔹 Limit on how many items can exist
    private int currentItemCount = 0;

    private float itemSpawnPeriod = 5f;
    private WaitForSeconds wait;

    private GameObject[] itemSpawnPoints;

    void Start()
    {
        itemSpawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawn");
        wait = new WaitForSeconds(itemSpawnPeriod);

        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        // Prevent spawning more than allowed
        if (currentItemCount >= maxItemCount)
            yield break;

        yield return wait;

        // Pick a random spawn point
        GameObject point = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)];

        int value = Random.Range(0, 2);

        float spread = 3f; // increase this for even more spacing

        Vector3 offset = new Vector3(
            Random.Range(-spread, spread),
            0,
            Random.Range(-spread, spread)
        );

        if (value == 0)
        {
            HealItem item = Instantiate(
                healItem,
                point.transform.position + offset,
                point.transform.rotation
            );

            currentItemCount++;

            item.onDestroy += () =>
            {
                currentItemCount--;
                StartCoroutine(SpawnItem());
            };
        }
        else
        {
            AmmoItem item = Instantiate(
                ammoItem,
                point.transform.position + offset,
                point.transform.rotation
            );

            currentItemCount++;

            item.onDestroy += () =>
            {
                currentItemCount--;
                StartCoroutine(SpawnItem());
            };
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
