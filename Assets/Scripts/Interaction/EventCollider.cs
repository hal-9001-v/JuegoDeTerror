using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class EventCollider : Interactable
{
    public UnityEvent atEnterEvent;

    public override void interact()
    {
        //Do nothing
    }

    public override void loadData(InteractableData myData)
    {
        done = myData.interactionDone;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eventOnlyOnce && !done)
            if (other.tag == "Player")
            {
                done = true;
                atEnterEvent.Invoke();

            }
    }
}
