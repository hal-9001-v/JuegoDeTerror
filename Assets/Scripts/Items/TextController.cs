using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextController : MonoBehaviour
{
    TextBox box;

    bool readyToCall = true;

    // bool skipLine;

    private void Awake()
    {
        box = FindObjectOfType<TextBox>();
    }


    public bool displayText(string[] sentences, float delay, UnityEvent endEvents)
    {

        if (readyToCall)
        {
            //skipLine = false;
            StartCoroutine(typeText(sentences, delay, endEvents));
            return true;
        }

        //else
        //skipLine = true;

        return false;

    }


    IEnumerator typeText(string[] sentences, float delay, UnityEvent endEvents)
    {
        box.show();
        readyToCall = false;

        foreach (string s in sentences)
        {

            char[] characters = s.ToCharArray();
            box.textMesh.text = "";


            for (int i = 0; i < characters.Length; i++)
            {
                box.textMesh.text += characters[i];

                //if (!skipLine)
                yield return new WaitForSeconds(delay);
            }
            //skipLine = false;

            yield return new WaitForSeconds(0.5f);

        }
        box.textMesh.text = "";
        box.hide();
        readyToCall = true;

        if (endEvents != null)
            endEvents.Invoke();
    }

}
