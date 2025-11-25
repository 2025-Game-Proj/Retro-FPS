// TimerStarter.cs
using UnityEngine;

public class TimerStarter : MonoBehaviour
{
    private void Start()
    {
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StartTimer();
        }
    }
}
