using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Radio : Item
{

    RadioTracker tracker;
    RadioController radioController;

    bool readyToCall = true;

    bool skipLine;

    private void Awake()
    {
        tracker = FindObjectOfType<RadioTracker>();
        radioController = FindObjectOfType<RadioController>();

    }

    public override void useItem()
    {
        if (tracker != null && radioController != null)
        {

            if (readyToCall)
            {
                skipLine = false;
                StartCoroutine(typeText());
            }

            else
                skipLine = true;

        }

    }

    
    IEnumerator typeText()
    {
        radioController.show();
        readyToCall = false;

        string[] sentences;
        float delay;

        if (tracker.currentZone != null)
        {
            sentences = tracker.currentZone.Text;
            delay = tracker.currentZone.delay;

            tracker.currentZone.startEvent.Invoke();

            tracker.currentZone.done = true;

        }
        else {
            sentences = new string[1];

            sentences[0] = "...";
            delay = 0.1f;
        }

        foreach (string s in sentences)
        {

            char[] characters = s.ToCharArray();
            radioController.textMesh.text = "";


            for (int i = 0; i < characters.Length; i++)
            {
                radioController.textMesh.text += characters[i];

                if (!skipLine)
                    yield return new WaitForSeconds(delay);
            }
            skipLine = false;

            yield return new WaitForSeconds(0.5f);

        }
        radioController.textMesh.text = "";

        if (tracker.currentZone != null) {
            tracker.currentZone.endEvent.Invoke();
        }



        radioController.hide();
        readyToCall = true;
    }

}
