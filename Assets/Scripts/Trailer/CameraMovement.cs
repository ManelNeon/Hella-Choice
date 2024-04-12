using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity;
    public float slowSpeed;
    public float normalSpeed;
    public float sprintSpeed;
    float currentSpeed;

    void Update()
    {
        if (Input.GetMouseButton(1)) //if we are holding right click
        {
            if (Input.GetMouseButton(0))
            {
                Rotation();
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Movement();
        }
        else if (!Input.GetMouseButton(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("OutsideCam");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.position = new Vector3(transform.position.x, (transform.position.y - 1), transform.position.z);
        }
    }

    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(0, 0, 0);
        mouseInput = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}

