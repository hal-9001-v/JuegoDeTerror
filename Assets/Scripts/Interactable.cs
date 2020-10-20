using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public Light myLight;

    float maxIntensity;

    Coroutine myCoroutine;

    bool haveLight;

    public UnityEvent interactionActions;
    //Execute Event only once
    public bool eventOnlyOnce;

    //Event has been executed
    public bool done { get; protected set; }

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

    public InteractableData getSaveData() {

        return new InteractableData(gameObject.name, done);
    }

    public abstract void loadData(InteractableData myData);

    public void invokeInteractionActions()
    {
        interact();

        if (eventOnlyOnce && done)
            return;

        done = true;

        interactionActions.Invoke();
    }

    public void selectForInteraction(bool b)
    {

        if (b)
        {
            highLight(true);
        }
        else
        {
            highLight(false);
        }
    }

    public void highLight(bool b)
    {
        if (haveLight)
        {
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
        bool direction = true;

        myLight.intensity = 1;

        do
        {
            if (myLight.intensity > 0.0f && myLight.intensity < maxIntensity)
            {
                if (direction)
                {
                    myLight.intensity += 1.0f;
                }
                else
                {
                    myLight.intensity -= 1.0f;
                }
            }
            else
            {
                if (direction)
                {
                    myLight.intensity -= 1.0f;
                }
                else
                {
                    myLight.intensity += 1.0f;
                    yield return new WaitForSeconds(1.0f);
                }

                direction = !direction;
            }
            yield return new WaitForSeconds(0.02f);

        } while (true);
    }

}
