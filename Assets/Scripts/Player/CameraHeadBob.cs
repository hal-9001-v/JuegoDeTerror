using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadBob : PlayerComponent
{
    public float bobbingSpeed = 1f;
    public float runningSpeed = 1.5f;
    public float bobbingAmountY = 0.05f;
    public float bobbingAmountX = 0.05f;

    
    public PlayerMovement playerController;
    private float footFall;

    public AudioClip footsteps;
    public AudioSource audioSource;

    public Vector3 defaultPosition { private set; get; }
    float timer = 0;

    
    private void Awake()
    {
        defaultPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(playerController.moveInput.x) > 0.1f || Mathf.Abs(playerController.moveInput.y) > 0.1f) && playerController.controller.isGrounded)
        {
            if (playerController.moveInput.x > 0.1f)
            {
                Mathf.Clamp(1.0f, -2.0f, 2.0f);
            }

            timer += Time.deltaTime * bobbingSpeed * runningSpeed ;


            transform.localPosition = new Vector3(defaultPosition.x + Mathf.Cos(timer) * bobbingAmountX, defaultPosition.y + Mathf.Sin(timer) * bobbingAmountY, transform.localPosition.z);

            footFall = Mathf.Cos(timer);
            if (footFall < 0)
            {
                audioSource.Play();
            }
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosition.y, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
        }


    }

    public override void setPlayerControls(PlayerControls pc)
    {

        pc.Normal.Move.performed += ctx => runningSpeed = ctx.ReadValue<Vector2>().magnitude;

        pc.Normal.Move.canceled += ctx => runningSpeed = 0;
    }
}
