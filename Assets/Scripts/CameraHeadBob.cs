using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadBob : MonoBehaviour
{
    public float bobbingSpeed = 14f;
    public float runningSpeedIncrease = 1.5f;
    public float bobbingAmountY = 0.05f;
    public float bobbingAmountX = 0.05f;
    public PlayerMovement playerController;
    private float footFall;

    public AudioClip footsteps;
    public AudioSource audioSource;

    float defaultPosY = 0;
    float defaultPosX = 0.0f;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        defaultPosX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(playerController.move.x) > 0.1f || Mathf.Abs(playerController.move.z) > 0.1f) && playerController.controller.isGrounded)
        {
            if (playerController.move.x > 0.1f)
            {
                Mathf.Clamp(1.0f, -2.0f, 2.0f);
            }
            //Player is moving
            if (playerController.isRunning)
            {
                timer += Time.deltaTime * bobbingSpeed * runningSpeedIncrease;
            }
            else
            {
                timer += Time.deltaTime * bobbingSpeed;
            }

            transform.localPosition = new Vector3(defaultPosX + Mathf.Cos(timer) * bobbingAmountX, defaultPosY + Mathf.Sin(timer) * bobbingAmountY, transform.localPosition.z);

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
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
        }
    }
}
