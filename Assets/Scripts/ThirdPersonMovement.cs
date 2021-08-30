using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    /// <summary>
    /// Use to controle the character movement.
    /// </summary>
    [SerializeField]
    private CharacterController controller;  
    
    /// <summary>
    /// Active the appropriate animation for the movement.
    /// </summary>
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float walkSpeed = 6.0f;   
    
    [SerializeField]
    private float runSpeed = 12.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float turnSmoothTime = 0.1f;
    
    [SerializeField]
    private Transform cam;

    private float turnSmoothVelocity;

    private bool groundedPlayer;
    private Vector3 playerVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        if (gameObject.GetComponent<CharacterController>() == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
        else
        {
            controller = gameObject.GetComponent<CharacterController>();
        }
    }
   
    private void Update()
    {
        // Check if the character is not in the mid air.
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        
        SetFalseAnimation();
        if (direction.magnitude >= 0.1f)
        {
            // Turn the character with the camera view.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("Running", true);
                controller.Move(moveDir.normalized * runSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walking", true);
                controller.Move(moveDir.normalized * walkSpeed * Time.deltaTime);
            }
        }

        // Changes the height position of the player.
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetTrigger("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void SetFalseAnimation()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
    }
}
