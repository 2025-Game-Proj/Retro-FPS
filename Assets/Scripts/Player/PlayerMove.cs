using UnityEngine;
using UnityEngine.TextCore.Text;

// 플레이어 이동을 담당하는 클래스
public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 10f;

    // 관성 감쇠 값 (멈출 때 부드럽게 감속하기 위한 값)
    public float momentumDamping = 5f;

    // 캐릭터 컨트롤러 컴포넌트 참조
    private CharacterController mainCC; // main character controller


    public Animator anim;

    // 걷기 상태를 나타내는 플래그
    private bool isWalking;

    // 입력 벡터 (사용자의 입력을 저장)
    private Vector3 inputVector;

    // 실제 이동 벡터 (중력 포함)
    private Vector3 movementVector;


    private float gravity = -10;


    void Start()
    {
        mainCC = GetComponent<CharacterController>();
    }


    void Update()
    {

        GetInput();

        MovePlayer();

        anim.SetBool("isWalking", isWalking);
    }

    // 사용자 입력을 받아 처리하는 함수
    void GetInput()
    {
        // WASD 키 중 하나라도 눌렸는지 확인
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            // 수평(좌우)과 수직(앞뒤) 입력을 받아 벡터 생성
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            inputVector.Normalize();

            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            // 입력이 없을 때 부드럽게 감속 (관성 효과)
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, Time.deltaTime * momentumDamping);

            // 걷기 상태를 false로 설정
            isWalking = false;
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    // 실제로 플레이어를 이동시키는 함수
    void MovePlayer()
    {

        mainCC.Move(movementVector * Time.deltaTime);
    }
}