using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10.0f; //Velocidad al andar

    public float runningIncrease = 1.5f; //Multiplicador de velocidad al correr

    public float gravity = 9.8f;

    private float fallVelocity;

    public bool isInclined = false;

    public bool isRunning = false;

    public Vector3 move; //Vector velocidad

    //Fatigue variables
    public float maxRunningTime;

    public float minRecoverTime = 2.0f;

    public float fatigueRecoverTimeIncrease = 0.8f;

    public bool hasFatigue = false;

    private float fatigueCounter;

    private void Start()
    {
        fatigueCounter = 0.0f;
    }

    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (hasFatigue == false)
            {
                hasFatigue = Fatigue();
            }
            else
            {
                hasFatigue = FatigueRecover();
            }
        }
    }

    //Método que controla el contador de fatiga del jugador mientras este sea igual a false
    public bool Fatigue()
    {
        bool hasFatigue = false;

        if (isRunning)
        {
            fatigueCounter += Time.deltaTime;
        }

        if (isRunning == false && fatigueCounter > 0.1f)
        {
            fatigueCounter -= fatigueRecoverTimeIncrease * Time.deltaTime;
        }
        Debug.Log(fatigueCounter);

        if(fatigueCounter >= maxRunningTime)
        {
            hasFatigue = true;
        }

        return hasFatigue;
    }

    //Método que se llama cuando el ugador tiene fatiga y devuelve hasFatigue = false cuando haya pasado el tiempo mínimo de recuperación
    public bool FatigueRecover()
    {
        bool hasFatigue = true;

        if (fatigueCounter >= maxRunningTime - minRecoverTime)
        {
            fatigueCounter -= fatigueRecoverTimeIncrease * Time.deltaTime;
        }
        else
        {
            hasFatigue = false;
        }

        return hasFatigue;
    }

    //Para fuerzas constantes
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            move = (transform.right * x) + (transform.forward * z);

            SetGravity();

            if (Input.GetButton("Run") && hasFatigue == false)
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

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
    }
}
