using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10.0f; //Velocidad al andar

    public float runningIncrease = 1.5f; //Multiplicador de velocidad al correr

    public float gravity = 9.8f;

    public float fallVelocity;

    public bool isInclined = false;

    public bool isRunning = false;

    public Vector3 move; //Vector velocidad

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = (transform.right * x) + (transform.forward * z);

        SetGravity();

        if (Input.GetButton("Run"))
        {
            isRunning = true;
            controller.Move(move * speed * runningIncrease * Time.deltaTime);
        }
        else
        {
            isRunning = false;

            controller.Move(move * speed * Time.deltaTime);

        }

        if (IsStop(x, z))
        {
            if (Input.GetButton("LookLeft") && isInclined == false)
            {
                Debug.Log("IZQUIERDAAA");
                this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position * 50.0f, Time.deltaTime);
                isInclined = true;
            }
            else if (Input.GetButton("LookRight") && isInclined == false)
            {
                Debug.Log("DERECHAAAA");
                isInclined = true;
            }
            else if (Input.GetButtonUp("LookLeft"))
            {
                isInclined = false;
            }

            else if (Input.GetButtonUp("LookRight"))
            {
                isInclined = false;
            }
        }


    }

    public bool IsStop(float xMovement, float zMovement)
    {
        if (xMovement > -0.01f && xMovement < 0.01f && zMovement > -0.01f && zMovement < 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetGravity()
    {

        if (controller.isGrounded)
        {
            fallVelocity = (-1) * gravity * Time.deltaTime;
            move.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            move.y = fallVelocity;
        }
    }
}
