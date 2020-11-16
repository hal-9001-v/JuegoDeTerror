using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class EventCollider : Interactable
{
    public override void interact()
    {
        //Do nothing
    }

    //Interactions are done through collision
    public override void invokeInteractionActions()
    {
        //Do nothing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eventOnlyOnce && !done && readyForInteraction)
        {
            if (other.tag == "Player")
            {
                done = true;
                interactionActions.Invoke();

            }
        }


    }
}
