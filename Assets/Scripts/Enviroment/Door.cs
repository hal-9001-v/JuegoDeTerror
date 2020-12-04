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
        /*
        if (inventory != null && inventory.selectedItem != null)
        {
            if (inventory.selectedItem.name != neededKey.name) {
                Debug.Log("Nope");
                return;
            }
            
        }
        */
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
