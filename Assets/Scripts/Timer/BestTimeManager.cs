// BestTimeManager.cs
using UnityEngine;

public static class BestTimeManager
{
    private const string BestTimeKey = "BestTime";

    // 저장된 베스트 타임이 있는지 확인
    public static bool HasBestTime()
    {
        return PlayerPrefs.HasKey(BestTimeKey);
    }

    // 베스트 타임 가져오기 (없으면 아주 큰 값 리턴)
    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BestTimeKey, Mathf.Infinity);
    }

    // 새로운 기록 저장
    public static void SaveBestTime(float newTime)
    {
        PlayerPrefs.SetFloat(BestTimeKey, newTime);
        PlayerPrefs.Save(); // 저장 확정
    }
}
