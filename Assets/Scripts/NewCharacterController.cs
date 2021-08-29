using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterController : MonoBehaviour
{
    private Animator my_animator;

    private readonly float runningSpeed = 20.0f;
    private readonly float walkingSpeed = 10.0f;
    private readonly float rotationSpeed = 50.0f;
    private readonly float jumpHeight = 100.0f;
    private Vector2 rotation = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = true;
        my_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
        SetFalseAnimation();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z))
        {
            my_animator.SetBool("Running",true);
            transform.Translate(Vector3.forward * runningSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            my_animator.SetBool("Walking", true);
            transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            SetFalseAnimation();
            my_animator.SetTrigger("Jump");
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);
        }
    }

    private void SetFalseAnimation()
    {
        my_animator.SetBool("Walking", false);
        my_animator.SetBool("Running", false);
    }
}
