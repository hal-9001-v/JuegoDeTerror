﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public bool startGame;

    public GameObject player;
    PlayerBrain playerBrain;

    public UnityEvent atStartActions;
    public UnityEvent afterDeathActions;

    public Camera mainCamera;
    public Transform cameraTransform;
    public CameraHeadBob myHeadBob;

    public Image myImage;

    Animator playerAnimator;

    SaveManager mySaveManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerAnimator = player.GetComponentInChildren<Animator>();
        playerBrain = player.GetComponent<PlayerBrain>();

        if (player == null)
        {
            Debug.LogWarning("No player in Scene");
        }

    }



    private void Start()
    {
        if (startGame)
            restartGame();

        mySaveManager = FindObjectOfType<SaveManager>();
    }

    public void restartGame()
    {
        StopAllCoroutines();

        fadeScreen(true, 0f, 0.1f, atStartActions);

        playerAnimator.SetTrigger("Restore");

        mySaveManager.LoadGame();

        playerBrain.enablePlayer(true);
    }

    IEnumerator deathScreen(float startTime, float frameTime)
    {
        if (myImage != null) {
            yield return new WaitForSeconds(startTime);

            myImage.enabled = true;

            //Fade out (Get to Color)
            for (float i = 0; i < 1; i += 0.01f)
            {
                myImage.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(frameTime);
            }
        }
        

        restartGame();
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

    public void fadeScreen(bool fadeIn, float startTime, float frameTime, UnityEvent atEndActions)
    {
        StopAllCoroutines();
        StartCoroutine(FadeScreen(fadeIn, startTime, frameTime, atEndActions));
    }

    IEnumerator FadeScreen(bool fadeIn, float startTime, float frameTime, UnityEvent atEndActions)
    {
        yield return new WaitForSeconds(startTime);

        myImage.enabled = true;

        if (fadeIn)
        {
            //Fade in (Get to Visible)
            for (float i = 1; i > 0; i -= 0.01f)
            {
                myImage.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(frameTime);
            }

            myImage.enabled = false;

        }
        else
        {
            //Fade out(Get to Black)
            for (float i = 0; i < 1; i += 0.01f)
            {
                myImage.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(frameTime);

            }
        }

        if (atEndActions != null)
        {
            atEndActions.Invoke();
        }

    }

    public void killPlayer()
    {
        playerBrain.enablePlayer(false);

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
                    StartCoroutine(deathScreen(selectedAC.startWait, selectedAC.frameWait));

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
