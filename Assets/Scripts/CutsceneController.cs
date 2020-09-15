using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    GameObject player;

    PlayerMovement myPlayerMovement;
    CameraLook myCameraLook;
    CameraHeadBob myCameraHeadBob;

    Camera mainCamera;

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

        mainCamera = StaticTool.GetComponentInAll<Camera>(player);

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

        playerAnimator.SetTrigger("Death");
        playerAnimator.SetInteger("DeathNumber", 1);
    }

}
