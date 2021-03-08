using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : PlayerComponent
{

    Camera[] myCameras;

    public Canvas gameCanvas;
    public AudioSource source;

    int currentCamera = 0;

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.ChangeCamera.performed += ctx =>
        {
            changeCamera(ctx.ReadValue<float>());
        };

        pc.Normal.HideCanvas.performed += ctx =>
        {
            if (gameCanvas.enabled)
                gameCanvas.enabled = false;
            else
                gameCanvas.enabled = true;

        };
    }

    void changeCamera(float value)
    {
        if (value > 0)
        {
            currentCamera++;

            if (currentCamera == myCameras.Length)
                currentCamera = 0;
        }

        else
        {

            currentCamera--;

            if (currentCamera < 0)
                currentCamera = myCameras.Length - 1;
        }


        foreach (Camera c in myCameras)
        {
            c.enabled = false;
        }

        myCameras[currentCamera].enabled = true;

        Debug.Log("Current Camera: " + myCameras[currentCamera].name);

        if (source != null)
            source.Play();


    }

    // Start is called before the first frame update
    void Awake()
    {
        myCameras = FindObjectsOfType<Camera>();

    }

}
