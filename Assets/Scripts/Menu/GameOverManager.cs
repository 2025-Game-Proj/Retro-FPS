// GameOverManager.cs
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] GameObject panel;        // GameOverPanel
    [SerializeField] Button firstSelected;    // 기본 포커스 버튼(Main Menu)
    [SerializeField] string mainMenuSceneName = "MainMenu"; // 메인 메뉴 씬 이름

    bool isShown;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (panel != null) panel.SetActive(false);
    }

    public void Show()
    {
        if (isShown) return;
        isShown = true;

        // 게임 정지
        Time.timeScale = 0f;

        // 마우스 커서 보이기
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // UI 켜기
        panel.SetActive(true);

        // 선택 포커스 설정(패드/키보드 내비게이션용)
        if (firstSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
        }
    }

    // 버튼 OnClick에 연결
    public void OnClickMainMenu()
    {
        // 복귀 전 원상복구
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName); // ★ 메인 메뉴로
    }

    // 선택) 다시 시작 버튼
    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 선택) 종료 버튼
    public void OnClickQuit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
