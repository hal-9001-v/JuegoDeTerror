using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : Trigger
{

    [TextArea(0, 3)]
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
        if (onlyOnce && done || !readyForInteraction)
        {
            return;
        }

        if (textController != null)
        {
            if (textController.displayText(sentences, delay, endEvent))
                done = true;
        }


    }
}

