using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    //Highligthing only works if a light is assigned on inspector. Otherwise, nothing will happen.

    public Light myLight;

    protected float maxIntensity;

    Coroutine myCoroutine;

    protected bool haveLight;

    public UnityEvent interactionActions;
    //Execute Event only once
    public bool eventOnlyOnce;

    public bool readyForInteraction = true;

    //Event has been executed
    public bool done;

    private void Awake()
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

    }

    public abstract void interact();

    public virtual void loadData(InteractableData myData)
    {
        done = myData.interactionDone;
        readyForInteraction = myData.readyForInteraction;
    }

    public virtual void invokeInteractionActions()
    {
        if (readyForInteraction)
        {
            if (eventOnlyOnce && done)
                return;

            interact();

            done = true;

            interactionActions.Invoke();
        }
    }

    public void selectForInteraction(bool b)
    {
        highLight(b);
    }

    public void highLight(bool b)
    {
        if (haveLight)
        {
            myLight.enabled = b;


            if (b)
            {
                myCoroutine = StartCoroutine(Highlighting());

            }
            else
            {
                StopCoroutine(myCoroutine);
            }
        }

    }

    public IEnumerator Highlighting()
    {
        float changeFactor = maxIntensity * 0.1f;
        float highLightValue = changeFactor;

        myLight.intensity = maxIntensity;

        do
        {

            if (myLight.intensity <= 0)
            {
                highLightValue = changeFactor;
            }
            else if (myLight.intensity >= maxIntensity)
            {
                highLightValue = -changeFactor;
            }

            myLight.intensity += highLightValue;

            yield return new WaitForSeconds(0.1f);

        } while (true);
    }

    public InteractableData getSaveData()
    {
        return new InteractableData(gameObject.name, done, readyForInteraction);
    }

    public void setReadyForInteraction(bool b)
    {
        readyForInteraction = b;
    }

    public void debugInteraction()
    {
        Debug.Log("Interaction with " + gameObject.name);
    }
}
