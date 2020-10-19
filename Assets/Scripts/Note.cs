﻿using System.Collections;
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

    private int counter = 1;
    private string provisional;
    private string result = "";                                               //Resultado de todos los párrafos de la nota

    private void Awake()
    {

        loadText();

        if (myNoteReader == null)
        {
            myNoteReader = FindObjectOfType<NoteReader>();
        }
    }

    void loadText()
    {
        provisional = textKey + "P" + counter;

        while (LanguageController.GetTextInLanguage(provisional) != provisional)
        {
            paragraphs.Add(LanguageController.GetTextInLanguage(provisional));
            counter++;
            provisional = textKey + "P" + counter;
        }


        for (int i = 0; i < paragraphs.Count; i++)
        {
            result += paragraphs[i] + "\n\n";
        }

    }

    public override void interact()
    {
        //myNoteReader.startReading(result);
        myNoteReader.startReading("HEY");
        done = true;
    }

    public override void loadData(InteractableData myData)
    {
        done = myData.interactionDone;

        if (done && eventOnlyOnce) { 
            //Do some stuff

        }
    }
}