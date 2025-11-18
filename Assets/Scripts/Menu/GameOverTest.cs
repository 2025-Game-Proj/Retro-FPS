using UnityEngine;
using UnityEngine.SceneManagement;

// 겜 조작용 스크립트
// 추후 삭제
public class GameOverTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("GameOver");  // GameOver 씬 이름 그대로
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("GameClear");  // GameClear 씬 이름 그대로
        }
    }
}
