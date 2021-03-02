using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : Trigger
{
    public UnityEvent events;

    public void trigger()
    {
        if (onlyOnce && done)
        {
            return;
        }
        events.Invoke();

        done = true;
    }



}
