// TimerManager.cs
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    // 다른 씬으로 넘어가도 유지되게 설정
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   // 이 오브젝트는 씬 전환 시에도 삭제 안 됨
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }
}
