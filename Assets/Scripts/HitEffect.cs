using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem effect;

    public void Play()
    {
        effect.gameObject.SetActive(true);
        effect.Play();
    }
}
