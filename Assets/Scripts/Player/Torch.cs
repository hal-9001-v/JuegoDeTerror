using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Torch : PlayerComponent
{
    public Light myLight;
    AudioSource audioSource;

    [Range(0, 5)]
    public float onPitch;

    [Range(0, 5)]
    public float offPitch;


    public bool readyToUse { private set; get; }

    float positionTimer;

    [Header("Torch Bob")]
    [Range(0, 1)]
    public float bobbingSpeed = 1;

    [Range(0, 1)]
    public float bobbingAmountX = 1;

    [Range(0, 1)]
    public float bobbingAmountY = 1;

    [Space(4)]
    [Header("Intensity")]
    [Range(5, 30)]
    public float maxTime;
    float currentTime;

    [Range(0, 5)]
    public float minIntensity;

    [Range(0, 5)]
    public float recoverRatio;

    float defaultIntensity;



    Vector3 defaultPosition;

    Pursuer pursuer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pursuer = FindObjectOfType<Pursuer>();

        if (myLight != null)
        {
            myLight.enabled = false;
            
        }
        else
        {
            myLight = GetComponent<Light>();

            if (myLight != null)
            {
                myLight.enabled = false;

            }
        }

        currentTime = maxTime;

        defaultPosition = transform.localPosition;
        defaultIntensity = myLight.intensity;
    }

    void Update()
    {
        if (myLight.enabled)
        {
            #region Light Intensity waste
            currentTime -= Time.deltaTime;

            myLight.intensity = defaultIntensity * currentTime / maxTime;

            if (myLight.intensity < minIntensity)
                myLight.intensity = minIntensity;


            if (currentTime < 0)
            {
                currentTime = 0;
            }


            #endregion

            positionTimer += Time.deltaTime * bobbingSpeed;

            transform.localPosition = new Vector3(defaultPosition.x + Mathf.Cos(positionTimer) * bobbingAmountX, defaultPosition.y + Mathf.Sin(positionTimer) * bobbingAmountY, transform.localPosition.z);

            if (positionTimer >= 360)
                positionTimer = 0;
        }
        else
        {
            #region Intensity Recover
            currentTime += Time.deltaTime*recoverRatio;

            if (currentTime > maxTime)
            {
                currentTime = maxTime;
            }
            #endregion
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pursuer != null)
        {
            pursuer.torchIsLit = false;
        }
    }

    void switchLight()
    {
        if (readyToUse)
        {
            if (myLight.enabled == false)
            {
                myLight.enabled = true;
                currentTime -= maxTime*0.1f;

                if (pursuer != null)
                {
                    pursuer.torchIsLit = true;
                }

                if (audioSource != null)
                {
                    audioSource.Play();
                    audioSource.pitch = onPitch;
                }

            }
            else
            {
                myLight.enabled = false;
                pursuer.torchIsLit = false;

                if (audioSource != null)
                {
                    audioSource.Play();
                    audioSource.pitch = offPitch;
                }


            }


        }

    }

    public void setReadyToUse(bool b)
    {
        readyToUse = b;
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Light.performed += ctx => switchLight();
    }

}
