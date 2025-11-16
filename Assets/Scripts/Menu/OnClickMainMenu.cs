using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu"; // 메인 메뉴 씬 이름

    public void GoToMainMenu()
    {
        // 게임 오버나 일시정지에서 Time.timeScale이 0일 수 있으니 미리 원복
        Time.timeScale = 1f;

        Debug.Log("[OnClickMainMenu] 버튼 눌림, 메인 메뉴 로드 시도");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
