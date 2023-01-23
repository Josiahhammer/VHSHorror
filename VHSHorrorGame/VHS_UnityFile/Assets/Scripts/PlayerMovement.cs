using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float mouseSensitivity = 1000f;
    public float jumpHeight = 100f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public Light flashlight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
/*        print(mouseX);
        print(mouseY);*/
        xRotation -= mouseY;
        yRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.GetChild(1).localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, -yRotation, 0f);
        //Camera.main.transform.Rotate(Vector3.up * mouseX);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDirection = transform.right * x + transform.forward * z;
        moveDirection.y = gravity;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
