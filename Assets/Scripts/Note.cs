using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Note : Interactable
{
    public static NoteReader myNoteReader;

    public string textKey;                                                    //Letras en común de las claves de los párrafos de la Hashtable
    public List<string> paragraphs = new List<string>();                      //Lista de todos los párrafos

    private string result = "";                                               //Resultado de todos los párrafos de la nota

    public TextAsset myTextAsset;

    private new void Awake()
    {

        if (myLight != null)
        {
            haveLight = true;
            maxIntensity = myLight.intensity;
        }
        else
        {
            haveLight = false;
        }

        //loadText();

        if (myTextAsset != null)
            result = myTextAsset.text;


        if (myNoteReader == null)
        {
            myNoteReader = FindObjectOfType<NoteReader>();
        }
    }

    public override void interact()
    {
        myNoteReader.startReading(result);

        done = true;
    }

    public override void loadData(InteractableData myData)
    {
        done = myData.interactionDone;

        if (done && eventOnlyOnce)
        {
            //Do some stuff

        }
    }
}