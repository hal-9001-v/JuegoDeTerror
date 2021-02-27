using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInteractable : Interactable
{

    public new ParticleSystem particleSystem;
    /*
     Only Executes Events on Inspector
     */

    public override void interact()
    {
        //Do nothing, just execute events
    }

    private void Update()
    {
        if (particleSystem != null) {
            if (hideWhenDone && done || !readyForInteraction)
            {
                if (particleSystem.isPlaying)
                {
                    particleSystem.Stop();
                    particleSystem.Clear();
                }
            }
            else {
                if (!particleSystem.isPlaying) {
                    particleSystem.Play();
                    
                }
            }
        }
    }

}
