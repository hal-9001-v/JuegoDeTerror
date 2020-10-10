using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    public static PlayerMovement sharedInstance;

    public CharacterController controller;

    public float speed = 10.0f; //Velocidad al andar

    public float runningIncrease = 1.5f; //Multiplicador de velocidad al correr

    public float gravity = 9.8f;

    private float fallVelocity;

    public bool isInclined = false;

    public bool isRunning = false;

    public bool isReading = false;

    //Fatigue variables
    public float maxRunningTime;

    public float minRecoverTime = 2.0f;

    public float fatigueRecoverTimeIncrease = 0.8f; //Multiplicador de velocidad de tiempo de recuperación

    public bool hasFatigue = false;

    private float fatigueCounter;

    bool run;

    public Vector2 moveInput;

    private void Awake()
    {
        sharedInstance = this;
    }


    private void Start()
    {
        fatigueCounter = 0.0f;
    }

    private void FixedUpdate()
    {
        makeMovement();
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
        //Debug.Log(fatigueCounter);

        if (fatigueCounter >= maxRunningTime)
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

    private void makeMovement()
    {
        if (isReading == false)
        {

            if (hasFatigue == false)
            {
                hasFatigue = Fatigue();
            }
            else
            {
                hasFatigue = FatigueRecover();
            }

            SetGravity();

            Vector3 move;

            move = (transform.right * moveInput.x) + (transform.forward * moveInput.y) + Vector3.up * fallVelocity;

            if (run && hasFatigue == false)
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


    public void SetGravity()
    {

        if (controller.isGrounded)
        {
            fallVelocity = (-1) * gravity * Time.deltaTime;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();

        pc.Normal.Move.canceled += ctx => moveInput = Vector2.zero;

        pc.Normal.Run.performed += ctx => run = true;

        pc.Normal.Run.canceled += ctx => run = false;


    }
}
