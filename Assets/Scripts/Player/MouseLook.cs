using System;
using Unity.Mathematics;
using UnityEngine;

// 마우스를 이용한 카메라/플레이어 회전을 담당하는 클래스
public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1.5f;    // 마우스 감도 
    public float smoothing = 1.5f;     // 부드러운 움직임을 위한 스무딩 값

    private float xMousePos;
    private float smoothedMousePos;

    private float currentLookingPos;     // 현재 바라보고 있는 각도


    private void Start()
    {
        // 마우스 커서를 화면 중앙에 고정
        Cursor.lockState = CursorLockMode.Locked;

        // 마우스 커서를 숨김
        Cursor.visible = false;
    }


    void Update()
    {
        if (PauseManagerInputSystem.IsPaused)
            return;

        // 마우스 입력 받기
        GetInput();

        // 입력 값 보정 및 스무딩 적용
        ModifyInput();

        // 플레이어 회전 실행
        MovePlayer();
    }


    void GetInput()
    {
        // 마우스 X축(좌우) 움직임 값을 가져옴
        xMousePos = Input.GetAxisRaw("Mouse X");
    }


    void ModifyInput()
    {
        // 마우스 입력에 감도와 스무딩 값을 곱함
        xMousePos *= sensitivity * smoothing;

        smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1f / smoothing);    // Lerp를 사용하여 부드러운 움직임 구현 현재 값과 목표 값 사이를 보간하여 부드러운 전환 효과

    }

    void MovePlayer()
    {

        currentLookingPos += smoothedMousePos;         // 현재 회전 각도에 스무딩된 마우스 입력을 누적

        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);     // Y축을 기준으로 회전 (좌우 회전) Quaternion.AngleAxis를 사용하여 각도를 쿼터니언으로 변환

    }
}