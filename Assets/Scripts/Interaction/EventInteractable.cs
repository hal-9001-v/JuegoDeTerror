using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInteractable : Interactable
{
    public bool setInactive;

    public override void interact()
    {
        //Do nothing, just execute events
    }

    public override void loadData(InteractableData myData)
    {

        done = myData.interactionDone;
        readyForInteraction = myData.readyForInteraction;

        if (setInactive && eventOnlyOnce && done)
        {
            gameObject.SetActive(false);
        }
    }
}
