using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextInteractable : Interactable
{

    public string[] sentences;
    public float delay = 0.05f;
    public UnityEvent endEvent;


    TextController textController;

    private void Start()
    {
        textController = FindObjectOfType<TextController>();
    }

    public override void interact()
    {

        if (textController != null) {
            textController.displayText(sentences, delay, endEvent);
        }
    
    
    }
}
