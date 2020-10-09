using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;

    PlayerMovement myPlayerMovement;
    CameraLook myCameraLook;
    CameraHeadBob myCameraHeadBob;

    public Camera mainCamera;
    public Transform cameraTransform;
    public CameraHeadBob myHeadBob;

    public Image myImage;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("No player in Scene");
        }

        myCameraLook = player.GetComponent<CameraLook>();

        myCameraLook = StaticTool.GetComponentInAll<CameraLook>(player);

        if (myCameraLook == null)
        {
            Debug.LogWarning("Player has no CameraLook Component!");
        }

        myPlayerMovement = StaticTool.GetComponentInAll<PlayerMovement>(player);

        if (myPlayerMovement == null)
        {
            Debug.LogWarning("Player has no PlayerMovement Component!");
        }


        myCameraHeadBob = StaticTool.GetComponentInAll<CameraHeadBob>(player);

        if (myCameraHeadBob == null)
        {
            Debug.LogWarning("Player has no CameraHeadBob Component!");
        }

    }

    public void screenShake(float time, int iterations, float distance)
    {
        StartCoroutine(ScreenShake(0.5f, 0.5f, 20, 5));
    }

    IEnumerator ScreenShake(float bobbingAmountX, float bobbingAmountY, float bobbingSpeed, float time)
    {
        Vector3 originalPosition = myHeadBob.defaultPosition;

        float shakeTimer;
        float effectTimer = 0;

        while (effectTimer < time)
        {

            effectTimer += Time.deltaTime;
            shakeTimer = effectTimer * bobbingSpeed;

            cameraTransform.localPosition = new Vector3(originalPosition.x + Mathf.Cos(shakeTimer) * bobbingAmountX, originalPosition.x + Mathf.Sin(shakeTimer) * bobbingAmountY, originalPosition.z);

            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
    }

    public void fadeScreen(bool b, float startTime, float frameTime)
    {
        StartCoroutine(FadeScreen(b, startTime, frameTime));
    }

    IEnumerator FadeScreen(bool b, float startTime, float frameTime)
    {
        yield return new WaitForSeconds(startTime);

        myImage.enabled = true;

        if (b)
        {
            //Fade in
            for (float i = 10; i > 0; i -= 0.01f)
            {
                myImage.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(frameTime);

            }
        }
        else
        {
            //Fade out
            for (float i = 0; i < 10; i += 0.01f)
            {
                myImage.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(frameTime);

            }

        }

        myImage.enabled = false;
    }

    public void killPlayer()
    {

        Animator playerAnimator = player.GetComponent<Animator>();
        myCameraLook.enabled = false;
        myPlayerMovement.enabled = false;
        myCameraHeadBob.enabled = false;



        if (playerAnimator == null)
        {
            playerAnimator = player.GetComponentInChildren<Animator>();

            if (playerAnimator == null)
            {
                Debug.LogError("Player Has no Animation Component");

                return;
            }
        }

        List<AnimationCollider> readyACList = new List<AnimationCollider>();

        foreach (AnimationCollider animColl in player.GetComponentsInChildren<AnimationCollider>())
        {
            if (animColl.isReady)
            {
                readyACList.Add(animColl);
            }
        }

        if (readyACList.Count != 0)
        {
            AnimationCollider selectedAC = readyACList[Random.Range(0, readyACList.Count)];


            try
            {
                playerAnimator.SetTrigger("Death");
                playerAnimator.SetInteger("DeathNumber", selectedAC.animationID);

                if (selectedAC.fadeOut)
                {
                    fadeScreen(false, selectedAC.startWait, selectedAC.frameWait);

                }

            }
            catch
            {
                Debug.LogError("Error at Animation Controller variables");
            }
        }
        else
        {
            Debug.LogError("No Death Animation avaliable!");
        }
    }

}
