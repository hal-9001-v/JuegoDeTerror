using UnityEngine;

public class Door : Interactable
{

    Animator myAnimator;
    private bool doorIsOpen;
    private static KeyTaker keyTaker;

    public Key neededKey;

    protected void Awake()
    {
        if (keyTaker == null)
            keyTaker = FindObjectOfType<KeyTaker>();

        myAnimator = GetComponent<Animator>();

        if (myAnimator == null)
        {

            myAnimator = GetComponentInParent<Animator>();

            if (myAnimator == null)
            {
                myAnimator = GetComponentInChildren<Animator>();

                if (myAnimator == null)
                {
                    Debug.LogWarning("No Animator Component in Object");
                }
            }

        }

    }

    public override void loadData(InteractableData myData)
    {
        //No need to do anything
    }

    public override void interact()
    {
        if (neededKey != null)
            if (!keyTaker.takenKeys.Contains(neededKey))
                return;

        if (doorIsOpen)
        {
            doorIsOpen = false;
            myAnimator.SetTrigger("CloseDoor");
        }
        else
        {
            doorIsOpen = true;
            myAnimator.SetTrigger("OpenDoor");
        }
    }
}
