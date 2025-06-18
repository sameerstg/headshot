using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActionMain input; 
    [SerializeField] float runSpeed=2, walkSpeed=1, dashSpeed = 5;
    [SerializeField] bool isCrouching;
    private void Awake()
    {
        input = new();
        input.Enable();
        input.MainActionMap.OnMouseMove.performed += OnMouseMove;
        input.MainActionMap.Slide.performed += CrouchToggle;
    }

    private void CrouchToggle(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
        HandleCrouch();
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector2 drag = context.ReadValue<Vector2>();
        Debug.LogError(drag);
        transform.eulerAngles += new Vector3(-drag.y, drag.x);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
    }

    void Update()
    {
        HandleMovement();
        //HandleLookAt();
    }
    void HandleLookAt()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            transform.LookAt(hitInfo.point);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }
    }
    void HandleMovement()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            dir += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            dir += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            dir += Vector3.left;
        if (dir != Vector3.zero)
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(dir * Time.deltaTime * walkSpeed);
            else
                transform.Translate(dir * Time.deltaTime * runSpeed);
        HandleCrouch();
    }

    private void HandleCrouch()
    {
        transform.position = new Vector3(transform.position.x, isCrouching ? 1 : 2, transform.position.z);
    }

}
