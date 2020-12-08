using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Radio : Item
{

    RadioTracker tracker;
    public TextMeshProUGUI textMesh;
    bool readyToCall = true;
    

    private void Awake()
    {
        tracker = FindObjectOfType<RadioTracker>();

    }

    public override void useItem()
    {
        if (tracker != null && textMesh != null && readyToCall ) {
            StartCoroutine(typeText());
        }

    }


    IEnumerator typeText() {
        readyToCall = false;

        string[] sentences = tracker.text;

        foreach (string s in sentences) {

            char[] characters = s.ToCharArray();
            textMesh.text = "";


            for (int i = 0; i < characters.Length; i++)
            {
                textMesh.text += characters[i];

                yield return new WaitForSeconds(tracker.delay);
            }

            yield return new WaitForSeconds(0.5f);

        }
        textMesh.text = "";
        readyToCall = true;
    }
}
