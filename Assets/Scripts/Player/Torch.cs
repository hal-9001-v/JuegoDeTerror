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


    public bool readyToUse;

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
