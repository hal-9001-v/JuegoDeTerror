using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    /*
     keys work just like Interactable. The only difference is it will add this key to the list of keys in "keyTaker" component
     of player, which is used to open locked doors. "InteractionActions" are invoke aswell.

     NOTE: to make this key "dissapear", add this.setActive(false) in "interactionActions" on inspector.
     
     */

    static KeyTaker keyTaker;

    private void Awake()
    {
        if (keyTaker == null)
            keyTaker = FindObjectOfType<KeyTaker>();
    }

    public override void interact()
    {
        keyTaker.AddKey(this);

        Item keyItem = GetComponent<Item>();
        GameEventManager.sharedInstance.AddedItemToInventoryEvent(keyItem);
    }
}
