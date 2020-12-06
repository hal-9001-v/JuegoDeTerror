﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{

    /*
     a)How to make it work:
        -Object must be tagged as interactable

        -interactionActions are executed ONLY when object is interacted. Interaction happens when pressing Interaction button
        over an interactable object

        -Object NEEDS a collider and a mesh
     
    b)HighLighting:
        -Highligthing only works if a light is assigned on inspector. Otherwise, nothing will happen. 

        -HighLighting is enabled automatically when an object is ready to be interacted

    c)Properties:
        -Object can only be interacted if "readyForInteraction" is true.
        
        -If "eventOnlyOnce" is true, it can only be interacted once.
     

    NOTE: to make this Interactable "dissapear", add this.setActive(false) in "interactionActions" on inspector.

     */


    public Light myLight;

    protected float maxIntensity;

    Coroutine myCoroutine;

    protected bool haveLight;

    public UnityEvent interactionActions;
    //Execute Event only once
    public bool eventOnlyOnce;

    public bool readyForInteraction = true;

    //Event has been executed
    [HideInInspector]
    public bool done;

    static Transform playerTransform;

    [Range(1, 500)]
    public float highLightRange;
    bool isLit;

    protected void Awake()
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

        if (playerTransform == null)
            playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }


    protected void FixedUpdate()
    {
        if (haveLight && playerTransform != null)
        {

            if (Vector3.Distance(playerTransform.position, transform.position) < highLightRange)
            {
                highLight(true);
            }
            else
            {
                highLight(false);
            }

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
        //highLight(b);
        //This will be executed when player is staring at this object
    }

    public void highLight(bool b)
    {
        if (haveLight)
        {
            myLight.enabled = b;


            if (b)
            {

                if (!isLit)
                {
                    myCoroutine = StartCoroutine(Highlighting());
                    isLit = true;
                }

            }
            else
            {
                if (isLit)
                {
                    StopCoroutine(myCoroutine);
                    isLit = false;
                }


            }

        }
    }

    public IEnumerator Highlighting()
    {
        float changeFactor = maxIntensity * 0.05f;
        float highLightValue = changeFactor;

        myLight.intensity = 0.0f;

        yield return new WaitForSeconds(1.0f);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, highLightRange);
    }
}
