using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    void Awake()
    {
        // 이 스크립트가 붙은 같은 오브젝트의 Button을 가져온다
        var btn = GetComponent<Button>();

        // 혹시 에디터에서 걸어둔 리스너가 있으면 지워버리고
        btn.onClick.RemoveAllListeners();

        // 이 스크립트의 GoToMainMenu를 강제로 연결
        btn.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        Debug.Log("[MainMenuButton] 버튼 눌림, 메인 메뉴 로드 시도");

        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
