using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed, gravityScale, jumpForce, mouseSens;

    private float xVelocity, yVelocity, zVelocity;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // move the player
        xVelocity = Input.GetAxis("Horizontal") * (Time.deltaTime * moveSpeed);
        zVelocity = Input.GetAxis("Vertical") * (Time.deltaTime * moveSpeed);
        
        if (controller.isGrounded)
        {
            yVelocity = Mathf.Max(0, yVelocity);
            if (Input.GetButton("Jump"))
            {
                yVelocity = jumpForce;
            }
        }
        else
        {
            yVelocity -= Time.deltaTime * gravityScale;
        }
        controller.Move(transform.right * xVelocity +
                        Vector3.up * yVelocity +
                        transform.forward * zVelocity);
        
        // move the camera
        transform.Rotate(0,Input.GetAxis("Mouse X") * mouseSens, 0);
        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSens, 0, 0);
    }
}
