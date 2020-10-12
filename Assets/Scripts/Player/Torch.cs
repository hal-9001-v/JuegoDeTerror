using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : PlayerComponent
{
    public AudioClip torchOnOffSound;
    private Light torch;
    private AudioSource audioSource;

    public bool readyToUse;

    bool torchIsLit;

    // Start is called before the first frame update
    void Start()
    {
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
                audioSource.PlayOneShot(torchOnOffSound);
            }
        }

    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Light.performed += ctx => switchLight();
    }

}
