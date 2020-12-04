using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : PlayerComponent
{
    public AudioClip torchOnOffSound;
    private Light torch;
    private AudioSource audioSource;

    private Item torchItem;

    public bool readyToUse;

    bool torchIsLit;

    // Start is called before the first frame update
    void Start()
    {
        torchItem = GetComponent<Item>();
        torch = GetComponent<Light>();
        torch.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    void switchLight()
    {
        if (readyToUse)
        {
            torchIsLit = !torchIsLit;

            if (torchIsLit)
            {
                torch.enabled = true;
            }
            else
            {
                torch.enabled = false;

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
        pc.Normal.TestButton.performed += ctx =>
        {
            setReadyToUse(true);
            GameEventManager.sharedInstance.AddedItemToInventoryEvent(torchItem);
        };

        pc.Normal.TestButton2.performed += ctx =>
        {
            GameEventManager.sharedInstance.DeletedItemToInventoryEvent(torchItem);
        };
    }

}
