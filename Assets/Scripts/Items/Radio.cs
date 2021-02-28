using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Radio : Item
{

    TextController textController;
    RadioTracker tracker;

    private void Awake()
    {
        tracker = FindObjectOfType<RadioTracker>();
        textController = FindObjectOfType<TextController>();

    }

    public override void useItem()
    {
        if (tracker != null && textController != null && tracker.currentZone != null)
        {

            if (textController.displayText(tracker.currentZone.Text, tracker.currentZone.delay, tracker.currentZone.endEvent))
            {
                tracker.currentZone.done = true;
                tracker.currentZone.startEvent.Invoke();
            }
        }
        else
        {
            var s = new string[1];
            s[0] = "...";

            textController.displayText(s, 0.1f, null);


        }

    }

}
