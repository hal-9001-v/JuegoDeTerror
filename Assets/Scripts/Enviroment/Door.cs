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

    public bool locked = false;
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

        if (neededKey != null)
        {
            locked = true;
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
            if (locked)
            {
                if (neededKey != null)
                {
                    if (inventory != null && inventory.selectedItem == neededKey)
                    {
                        openDoor();
                        locked = false;
                        inventory.DeleteItem(neededKey);
                    }
                }

            }
            else
            {
                openDoor();
            }
        }
    }

    public void openDoor()
    {
        doorIsOpen = true;
        myAnimator.SetTrigger("OpenDoor");
    }

    public void closeDoor()
    {
        doorIsOpen = false;
        myAnimator.SetTrigger("CloseDoor");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, new Vector3(30, 30, 30));

    }
}
