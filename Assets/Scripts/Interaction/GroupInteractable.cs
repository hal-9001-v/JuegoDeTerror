using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupInteractable : MonoBehaviour
{

    /*
     * Waits for all EventInteractable Components in CHILDREN to invoke UnityEvent "interactionEvent"
     */
    bool done;
    public bool onlyOnce;

    int interactionMax;
    int interactionCounter;

    public UnityEvent interactionEvent;

    Interactable[] myInteractables;

    private void Start()
    {
        interactionMax = 0;
        interactionCounter = 0;

        myInteractables = GetComponentsInChildren<Interactable>();

        foreach (Interactable child in myInteractables)
        {
            interactionMax++;

            child.interactionActions.AddListener(addInteraction);
        }
    }

    void addInteraction()
    {
        interactionCounter++;

        if (interactionCounter >= interactionMax)
        {

            if (done && onlyOnce) return;

            done = true;

            interactionEvent.Invoke();

        }
    }

    public void loadData(InteractableData myData)
    {

        done = myData.interactionDone;

        if (onlyOnce && done)
        {
            gameObject.SetActive(false);
        }
    }

    public void setReadyForInteraction(bool b)
    {

        if (myInteractables != null)
        {
            foreach (Interactable child in myInteractables)
            {
                child.setReadyForInteraction(b);
            }
        }
        else
        {
            Debug.LogWarning("No interactables on Interactable Group " + gameObject.name);
        }


    }
}
