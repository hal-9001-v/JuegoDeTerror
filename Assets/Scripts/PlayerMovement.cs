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
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
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
        }
    }

    /*
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
    */

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
