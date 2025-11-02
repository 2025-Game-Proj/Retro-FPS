using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float momentumDamping = 5f;
    private CharacterController mainCC; // main character controller 
    public Animator anim;
    private bool isWalking;

    private Vector3 inputVector;
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


    void GetInput()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, Time.deltaTime * momentumDamping);
            isWalking = false;
        }

        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    void MovePlayer()
    {
        mainCC.Move(movementVector * Time.deltaTime);
    }


}
