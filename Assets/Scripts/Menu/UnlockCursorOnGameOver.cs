using UnityEngine;

public class UnlockCursorOnGameOver : MonoBehaviour
{
    void OnEnable()
    {
        // 게임 오버 UI가 켜질 때 마우스 풀기
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
