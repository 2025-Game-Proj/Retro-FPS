using UnityEngine;
using UnityEngine.SceneManagement;  // 메뉴 씬 이동용

public class PauseManagerInputSystem : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject pauseUI;               // 일시정지 Canvas

    [Header("Options")]
    [SerializeField] private bool lockCursorWhenPlaying = true;

    [Header("Scene")]
    [SerializeField] private string menuSceneName = "MainMenu"; // 빌드 세팅에 등록된 메뉴 씬 이름

    private bool isPaused = false;

    private void Awake()
    {
        // 씬 시작 시 항상 정상 속도로
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        SetCursorForPlayState();
    }

    private void Update()
    {
        // 여기서 ESC 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// ESC로 토글할 때 사용하는 함수
    /// </summary>
    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    /// <summary>
    /// 게임을 일시정지
    /// </summary>
    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;
        IsPaused = true;

        isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (pauseUI != null)
            pauseUI.SetActive(true);

        SetCursorForPauseState();
    }

    /// <summary>
    /// 게임 재개 (Resume 버튼 OnClick에 연결)
    /// </summary>
    public void Resume()
    {
        if (!isPaused) return;
        isPaused = false;
        IsPaused = false;

        isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        SetCursorForPlayState();
    }

    /// <summary>
    /// 메인 메뉴로 이동 (ToMenu 버튼 OnClick에 연결)
    /// </summary>
    public void GoToMenu()
    {
        // 혹시 멈춰 있으면 풀고 이동
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (!string.IsNullOrEmpty(menuSceneName))
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            Debug.LogWarning("menuSceneName 이 비어 있습니다. Inspector에서 메뉴 씬 이름을 설정하세요.");
        }
    }

    // === 커서 관련 ===
    private void SetCursorForPauseState()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void SetCursorForPlayState()
    {
        if (lockCursorWhenPlaying)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
