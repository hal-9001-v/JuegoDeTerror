using UnityEngine;

public abstract class KeyLock : Interactable
{
    protected string keyWord;

    public Key[] myKeys;

    private bool open;

    public KeyTaker keyTaker;

    void Awake()
    {
        if (keyTaker == null)
        {
            keyTaker = FindObjectOfType<KeyTaker>();

            if (keyTaker == null)
            {
                Debug.LogWarning("No KeyTaker in scene!");
            }
        }

    }

    private void Start()
    {
        keyWord = gameObject.name + gameObject.GetInstanceID();

        foreach (Key k in myKeys)
        {
            k.myKeyWord = keyWord;
        }
    }

    public override void interact()
    {
        if (!open)
        {
            if (keyTaker != null)
            {
                //Check if can be open
                int numberOfKeys = 0;

                foreach (Key k in keyTaker.takenKeys)
                {
                    if (k.myKeyWord == keyWord)
                        numberOfKeys++;
                }

                //If all keys ara taken
                if (numberOfKeys == myKeys.Length)
                {
                    interactionAction();
                    open = true;
                }

            }
            else
            {
                //Already open and interacted
                interactionAction();
                Debug.LogWarning("KeyTaker is null");

            }
        }
    }

    protected abstract void interactionAction();

}
