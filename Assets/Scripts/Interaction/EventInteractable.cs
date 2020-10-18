using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : Interactable
{
    public UnityEvent atInteraction;

    public override void interact()
    {
        atInteraction.Invoke();
    }

}
