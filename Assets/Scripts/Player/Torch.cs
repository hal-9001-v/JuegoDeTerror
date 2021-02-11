using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : PlayerComponent
{
    public AudioClip torchOnOffSound;
    public Light myLight;
    private AudioSource audioSource;

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
            }
            else
            {
                myLight.enabled = false;
                pursuer.torchIsLit = false;

                //audioSource.PlayOneShot(torchOnOffSound);
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
