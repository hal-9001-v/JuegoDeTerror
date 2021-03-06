﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FootSteps))]
public class PlayerMovement : PlayerComponent
{
    public static PlayerMovement sharedInstance;

    [Header("Objects")]
    public CharacterController controller;

    public CameraHeadBob headBob;

    public Image bar;
    public Image exhaustionBar;

    FootSteps ft;

    public float speed = 10.0f; //Velocidad al andar

    public float runningIncrease = 1.5f; //Multiplicador de velocidad al correr

    public float gravity = 25f;

    private float fallVelocity;

    public bool isInclined = false;

    public bool isRunning = false;

    public bool isReading = false;

    //Fatigue variables
    public float maxRunningTime;

    public float minRecoverTime = 2.0f;

    public float fatigueRecoverTimeIncrease = 0.8f; //Multiplicador de velocidad de tiempo de recuperación

    public bool hasFatigue = false;

    float fatigueCounter;

    float exhaustion;

    [Range(0, 1)]
    public float exhaustionFactor;
    [Range(0, 1)]
    public float recoverFactor;



    bool run;

    bool canRun = true;

    bool canMove = true;

    public Vector2 moveInput;
    Vector3 lastMove;


    private void Awake()
    {
        sharedInstance = this;
        ft = GetComponent<FootSteps>();

        if (headBob == null)
            headBob = FindObjectOfType<CameraHeadBob>();


    }


    private void Start()
    {
        fatigueCounter = 0.0f;
    }

    private void FixedUpdate()
    {
        makeMovement();

        if (isRunning){

            if (exhaustion < 0.5f * maxRunningTime)
                exhaustion += Time.deltaTime * exhaustionFactor;
            else {
                exhaustion = 0.5f * maxRunningTime;
            }
        }
        else if(!hasFatigue)
        {
            exhaustion -= Time.deltaTime * recoverFactor;
            
            if (exhaustion < 0)
                exhaustion = 0;
        }


    }

    private void Update()
    {
        if (bar != null && exhaustionBar != null)
        {
            bar.fillAmount = (maxRunningTime - fatigueCounter - exhaustion) / maxRunningTime;
            exhaustionBar.fillAmount = exhaustion / maxRunningTime;

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
        else if (fatigueCounter > 0.1f)
        {
            fatigueCounter -= fatigueRecoverTimeIncrease * Time.deltaTime;

        }
        //Debug.Log(fatigueCounter);

        if (fatigueCounter + exhaustion >= maxRunningTime)
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

        if (!canMove)
        {

            headBob.setRunningSpeed(Vector2.zero);
            return;
        }

        headBob.setRunningSpeed(moveInput);

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
            if (moveInput != Vector2.zero)
            {

                Vector3 move;

                move = (transform.right * moveInput.x) + (transform.forward * moveInput.y) + Vector3.up * fallVelocity;

                move.Normalize();

                move = Vector3.Lerp(lastMove, move, Time.deltaTime * 2.5f);


                lastMove = move;


                if (run && canRun && !hasFatigue)
                {
                    isRunning = true;
                    controller.Move(move * speed * runningIncrease * Time.deltaTime);
                    ft.playRunning();
                }
                else
                {
                    isRunning = false;

                    controller.Move(move * speed * Time.deltaTime);
                    ft.playWalking();
                }



            }


        }

    }

    public void setCanRun(bool b)
    {
        canRun = b;
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


    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();

        pc.Normal.Move.canceled += ctx =>
        {
            lastMove = Vector2.zero;
            moveInput = Vector2.zero;

            isRunning = false;

        };

        pc.Normal.Run.performed += ctx => run = true;

        pc.Normal.Run.canceled += ctx => run = false;


    }

    public void delayControl(float time)
    {
        StopAllCoroutines();
        StartCoroutine(controlDelayCounter(time));
    }

    IEnumerator controlDelayCounter(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }



    public void restoreStamina() {
        exhaustion = 0;
        fatigueCounter = 0;

    }
}
