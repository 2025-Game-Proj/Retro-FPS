using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scenes")]
    //[SerializeField] private string gameSceneName = "Game";

    [Header("Optional Fade")]
    [SerializeField] private CanvasGroup fader; // 없으면 null로 둬도 됨
    [SerializeField] private float fadeDuration = 0.25f;

    // 인스펙터에서 게임 씬 이름을 넣어두면 안전
    [SerializeField] private string gameSceneName = "MainGame";

    public void OnClickStart()
    {
        if (fader != null)
            StartCoroutine(LoadWithFade());
        else
            SceneManager.LoadScene(gameSceneName);
    }

    // (선택) 비동기 로드 예시
    public void OnClickStartAsync()
    {
        StartCoroutine(LoadAsync());
    }
    private System.Collections.IEnumerator LoadAsync()
    {
        var op = SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Single);
        op.allowSceneActivation = true;
        while (!op.isDone) yield return null;
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에선 재생 중지
#elif UNITY_WEBGL
        // WebGL은 Application.Quit() 미지원. 종료 패널 표시 등으로 대체 권장.
        Debug.Log("Quit is not supported on WebGL.");
#else
        Application.Quit(); // PC/Android 등에서 정상 종료
#endif
    }

    // --- 아래는 선택: 페이드 전환 ---
    private System.Collections.IEnumerator LoadWithFade()
    {
        yield return Fade(1f); // 어두워짐
        var op = SceneManager.LoadSceneAsync(gameSceneName);
        while (!op.isDone) yield return null;
    }

    private System.Collections.IEnumerator Fade(float target)
    {
        if (fader == null) yield break;
        fader.blocksRaycasts = true;
        float start = fader.alpha, t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fader.alpha = Mathf.Lerp(start, target, t / fadeDuration);
            yield return null;
        }
        fader.alpha = target;
        fader.blocksRaycasts = target > 0.9f;
    }
}
