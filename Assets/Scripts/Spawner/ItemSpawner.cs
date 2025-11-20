using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public HealItem healItem;
    public AmmoItem ammoItem;
    private float itemSpawnPeriod = 30f;
    private WaitForSeconds wait;

    void Start()
    {
        wait = new WaitForSeconds(itemSpawnPeriod);
        StartCoroutine(SpawnItem());
    }

    IEnumerator SpawnItem()
    {
        yield return wait;

        int value = Random.Range(0, 2);
        if(value == 0)
        {
            HealItem item = Instantiate(healItem);
            item.onDestroy += ()=>{
                StartCoroutine(SpawnItem());
            };
        }
        else
        {
            AmmoItem item = Instantiate(ammoItem);
            item.onDestroy += ()=>{
                StartCoroutine(SpawnItem());
            };
        }
    }
    

    private void OnDestroy() {
        StopAllCoroutines();
    }
}
