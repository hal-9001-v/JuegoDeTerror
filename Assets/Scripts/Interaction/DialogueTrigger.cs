using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : Trigger
{

    public string[] sentences;
    public float delay = 0.05f;

    public UnityEvent endEvent;
    TextController textController;

    private void Start()
    {
        textController = FindObjectOfType<TextController>();
    }

    public void trigger()
    {
        if (onlyOnce && done)
        {
            return;
        }

        if (textController != null)
        {
            textController.displayText(sentences, delay, endEvent);
            done = true;
        }


    }
}

