
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 0.4f;
    public float gravity = 9.81f;
    public float airControl = 10;
    bool isGrounded;
    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    void Start()
    {
       controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        input = horizontal * transform.right + vertical * transform.forward;
        input.Normalize();

        if(controller.isGrounded)
        {
            moveDirection = input;

            if(Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpForce * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);

    }
}
