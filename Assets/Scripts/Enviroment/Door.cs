using UnityEngine;

public class Door : Interactable
{
    /*
     Door needs a key if "neededKey" is not null. Key must an object in scene.
     */

    Animator myAnimator;
    private bool doorIsOpen;

    ScrollInventory inventory;
    public Key neededKey;

    protected void Awake()
    {
        if (inventory == null)
            inventory = FindObjectOfType<ScrollInventory>();

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
        //Closing doors is always possible
        if (doorIsOpen)
        {
            closeDoor();

        }
        else
        {
            //If a key is needed and there is an inventory, check if key is selected in inventory
            if (inventory != null && neededKey != null)
            {

                if (neededKey == inventory.selectedItem)
                {
                    openDoor();
                }

            }
            //No key is needed, the door can be open every time
            else
            {
                openDoor();
            }


        }
    }

    void openDoor()
    {
        doorIsOpen = true;
        myAnimator.SetTrigger("OpenDoor");
    }

    void closeDoor()
    {
        doorIsOpen = false;
        myAnimator.SetTrigger("CloseDoor");
    }
}
