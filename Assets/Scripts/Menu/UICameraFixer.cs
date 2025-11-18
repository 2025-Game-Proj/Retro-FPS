using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class UICameraFixer : MonoBehaviour
{
    void Awake()
    {
        var canvas = GetComponent<Canvas>();
        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            if (canvas.worldCamera == null)
            {
                var cam = Camera.main;
                if (cam != null)
                {
                    canvas.worldCamera = cam;
                    Debug.Log("[UICameraFixer] Canvas worldCamera를 Camera.main으로 설정");
                }
                else
                {
                    Debug.LogWarning("[UICameraFixer] Camera.main을 찾을 수 없습니다.");
                }
            }
        }
    }
}
