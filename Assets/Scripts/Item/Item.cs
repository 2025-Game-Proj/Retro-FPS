using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float lifeTime = 180f;
    private Coroutine coroutine;
    private bool obtained = false;
    private void Start()
    {
        coroutine = StartCoroutine(DestroyAfterTime());
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        if (!obtained)
        {
            Destroy(gameObject);
        }
    }
    public abstract void OnObtained(GameObject player);
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            obtained = true;
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            OnObtained(other.gameObject);
            Destroy(gameObject);
        }
    }
    public delegate void DestroyCallback();
    public event DestroyCallback onDestroy;
    private void OnDestroy() {
        if(onDestroy != null)
        {
            onDestroy.Invoke();
        }
    }
}
