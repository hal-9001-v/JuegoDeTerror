using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : PlayerComponent
{
    public AudioClip torchOnOffSound;
    private Light torch;
    private AudioSource audioSource;

    bool readyToUSe;

    // Start is called before the first frame update
    void Start()
    {
        torch = GetComponent<Light>();
        torch.enabled = false;
        audioSource = GetComponent<AudioSource>();

        torch.enabled = false;
    }

    void enableLight(bool b)
    {
        if (readyToUSe)
        {
            if (b)
            {
                torch.enabled = true;
            }
            else
            {
                torch.enabled = false;
                audioSource.PlayOneShot(torchOnOffSound);
            }
        }

    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Light.performed += ctx => enableLight(true);

        pc.Normal.Light.canceled += ctx => enableLight(false);
    }

}
