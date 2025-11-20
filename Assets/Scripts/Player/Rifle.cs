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

    // --- Animation ---
    private Animator anim;


    // --- Config ---
    public float baseShotDelay = 1.0f;
    public float shotSpeed = 1.0f;
    private float shotDelay => baseShotDelay / shotSpeed;
    private float timeLastFired = 0.0f;
    public int maxAmmo = 300;
    public int ammo = 70;
    public int maxMagazine = 30;
    public int curMagazine = 30;
    public int attack = 10;
    public float range = 50.0f;
    private bool recoil = false;
    private bool reload = false;
    private Vector3 initialPosition;
    private LayerMask playerMask;
    private int coin = 0;

    void Start()
    {
        Reset();
        initialPosition = transform.localPosition;
        for (int i = 0; i < poolCapacity; i++)
        {
            hitParticlePool[i] = Instantiate(hitPrefab);
            hitParticlePool[i].gameObject.SetActive(false);
        }
        if (source != null)
        {
            source.clip = GunShotClip;
        }
        int mask = LayerMask.GetMask("Player", "Item");
        playerMask = ~mask;
        anim = GetComponent<Animator>();
        coin = 0;
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
        if (Input.GetMouseButton(0) && !reload && curMagazine > 0 && Time.time > timeLastFired + shotDelay)
        {
            Fire();
            recoil = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && !reload && !recoil && curMagazine < maxMagazine && ammo > 0)
        {
            anim.SetTrigger("Reload");
            reload = true;
        }
        if (recoil)
        {
            Recoil();
        }
    }

    private void Fire()
    {
        timeLastFired = Time.time;
        curMagazine--;
        muzzlePrefab.Play();
        if (source != null)
        {
            if (source.transform.IsChildOf(transform))
            {
                source.Play();
            }
        }
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range, playerMask))
        {
            if (hit.collider.TryGetComponent(out EnemyHealth hp))
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
    private void Reload()
    {
        reload = false;
        int reloadAmmo = Mathf.Min(ammo, maxMagazine - curMagazine);
        ammo -= reloadAmmo;
        curMagazine += reloadAmmo;
    }
    private void Recoil()
    {
        float elapsed = Time.time - timeLastFired;
        if (elapsed < 0.2f / shotSpeed)
        {
            float recoilDist = 0.5f;
            if (elapsed < 0.07f / shotSpeed)
            {
                float delta = recoilDist * (elapsed / (0.07f / shotSpeed));
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - delta);
            }
            else
            {
                float delta = recoilDist - (recoilDist * ((elapsed - 0.07f / shotSpeed) / (0.13f / shotSpeed)));
                transform.localPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - delta);
            }
        }
        else
        {
            transform.localPosition = initialPosition;
            recoil = false;
        }
    }
    public void AddAmmo(int amount)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
    }
    public void AddCoin()
    {
        coin++;
        if(coin % 10 == 0)
        {
            AddAttack(5);
            AddShotSpeed(0.3f);
        }
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
