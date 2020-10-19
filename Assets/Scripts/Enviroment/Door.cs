using UnityEngine;

public class Door : KeyLock
{

    Animator myAnimator;
    private bool doorIsOpen;



    protected void Awake()
    {
        myAnimator = GetComponent<Animator>();

        if (myAnimator == null)
        {
            Debug.LogWarning("No Animator Component in Object");
        }

    }

    protected override void interactionAction()
    {
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

    public override void loadData(InteractableData myData)
    {
        //No need to do anything
    }
}
