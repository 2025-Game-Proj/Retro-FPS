using UnityEngine;

public class Rifle : MonoBehaviour
{
    // --- Audio ---
    public AudioClip GunShotClip;
    public AudioSource source;
    public Vector2 audioPitch = new Vector2(.9f, 1.1f);

    // --- Objects ---
    public ParticleSystem muzzlePrefab;
    public ParticleSystem hitPrefab;
    private const int poolCapacity = 10;
    private ParticleSystem[] hitParticlePool = new ParticleSystem[poolCapacity];
    private int poolIdx = 0;
    public Camera playerCamera;

    // --- Config ---
    public float baseShotDelay = 1.0f;
    public float shotSpeed = 1.0f;
    private float shotDelay => baseShotDelay / shotSpeed;
    private float timeLastFired = 0.0f;
    public int ammo = 100;
    public int attack = 10;
    public float range = 50.0f;
    private Vector3 initialPosition;

    void Start()
    {
        Reset();
        initialPosition = transform.localPosition;
        for(int i = 0; i < poolCapacity; i++)
        {
            hitParticlePool[i] = Instantiate(hitPrefab);
            hitParticlePool[i].gameObject.SetActive(false);
        }
    }
    public void Reset()
    {
        ammo = 100;
        attack = 10;
        shotSpeed = 1.0f;
        timeLastFired = 0.0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && ammo > 0 && Time.time > timeLastFired + shotDelay)
        {
            Fire();
        }
        Recoil();
    }

    private void Fire()
    {
        timeLastFired = Time.time;
        ammo--;
        muzzlePrefab.Play();
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range))
        {
            if(hit.collider.TryGetComponent(out EnemyHealth hp))
            {
                hp.ApplyDamage(attack);
            }
            hitParticlePool[poolIdx].gameObject.SetActive(true);
            hitParticlePool[poolIdx].transform.position = hit.point + hit.normal * 0.1f;
            hitParticlePool[poolIdx].transform.forward = hit.normal;
            hitParticlePool[poolIdx].Play();
            poolIdx = (poolIdx + 1) % poolCapacity;
        }
    }
    private void Recoil()
    {
        float elapsed = Time.time - timeLastFired;
        if (elapsed < 0.2f / shotSpeed)
        {
            float recoil = 0.5f;
            if (elapsed < 0.07f / shotSpeed)
            {
                float delta = recoil * (elapsed / (0.07f / shotSpeed));
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - delta);
            }
            else
            {
                float delta = recoil - (recoil * ((elapsed - 0.07f / shotSpeed) / (0.13f / shotSpeed)));
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - delta);
            }
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
    public void AddAmmo(int amount)
    {
        ammo += amount;
    }
    public void AddAttack(int amount)
    {
        attack += amount;
    }
    public void AddShotSpeed(float amount)
    {
        shotSpeed += amount;
    }
}
