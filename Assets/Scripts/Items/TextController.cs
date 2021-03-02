using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextController : PlayerComponent
{
    TextBox box;

    bool readyToCall = true;

    bool textInteraction;

    bool fillLine;

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

        textInteraction = false;
        foreach (string s in sentences)
        {

            char[] characters = s.ToCharArray();
            box.textMesh.text = "";


            for (int i = 0; i < characters.Length; i++)
            {
                if (!textInteraction)
                {
                    box.textMesh.text += characters[i];

                    yield return new WaitForSeconds(delay);
                }
                else {
                    box.textMesh.text = characters.ArrayToString();
                    goto endOfLoop;
                }
            }

            endOfLoop:

            textInteraction = false;
            
            while (!textInteraction) {
                yield return null;
            }



        }
        box.textMesh.text = "";
        box.hide();
        readyToCall = true;

        if (endEvents != null)
            endEvents.Invoke();
    }


    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.TextInteraction.performed += ctx => {
            textInteraction = true;
        };


    }
}
