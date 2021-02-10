using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{

    public bool onlyOnce;
    public bool done;

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

    public void loadData(DialogueData myData)
    {
        done = myData.dialogueDone;

    }

    public DialogueData getSaveData() {

        return new DialogueData(name, done);
    }


}

