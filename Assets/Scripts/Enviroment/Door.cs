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
    public bool superLocked = false;

    public AudioClip openDoorSound;
    public AudioClip tryToOpenSound;

    AudioSource audioSource;

    new void Awake()
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

        if (openDoorSound != null && tryToOpenSound != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public override void loadData(InteractableData myData)
    {
        done = myData.interactionDone;
        locked = myData.doorLocked;

        if (myData.doorOpen)
        {
            openDoor();
        }
    }

    public override InteractableData getSaveData() {
        InteractableData myData = new InteractableData(name, done, readyForInteraction);
        myData.doorLocked = locked;
        myData.doorOpen = doorIsOpen;

        return myData;
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
            if (locked || superLocked)
            {
                if (neededKey != null)
                {
                    if (inventory != null && inventory.selectedItem == neededKey)
                    {
                        openDoor();
                        locked = false;
                        inventory.DeleteItem(neededKey);
                    }
                    else if (audioSource != null)
                    {
                        if (!audioSource.isPlaying)
                        {
                            audioSource.clip = tryToOpenSound;
                            audioSource.Play();
                        }

                    }
                }
                else if (audioSource != null)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = tryToOpenSound;
                        audioSource.Play();
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

        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = openDoorSound;
                audioSource.Play();
            }
        }
    }

    public void closeDoor()
    {
        doorIsOpen = false;
        myAnimator.SetTrigger("CloseDoor");

        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = openDoorSound;
                audioSource.Play();
            }
        }
    }

    public void setLock(bool b)
    {
        locked = b;
    }

    public void serSuperLock(bool b) {
        superLocked = b;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, new Vector3(30, 30, 30));

    }
}
