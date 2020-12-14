using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : PlayerComponent
{
    public AudioClip torchOnOffSound;
    private Light light;
    private AudioSource audioSource;

    public bool readyToUse;

    bool torchIsLit;

    Pursuer pursuer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        light = FindObjectOfType<Light>();

        pursuer = FindObjectOfType<Pursuer>();
        if (pursuer != null) {
            pursuer.torchIsLit = false;
        }
        light.enabled = false;

    }

    void switchLight()
    {
        if (readyToUse)
        {
            torchIsLit = !torchIsLit;

            if (torchIsLit)
            {
                light.enabled = true;

                if (pursuer != null) {
                    pursuer.torchIsLit = true;
                }
            }
            else
            {
                light.enabled = false;
                pursuer.torchIsLit = false;

                //audioSource.PlayOneShot(torchOnOffSound);
            }
        }

    }

    public void setReadyToUse(bool b) {
        readyToUse = b;
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Light.performed += ctx => switchLight();
    }

}
