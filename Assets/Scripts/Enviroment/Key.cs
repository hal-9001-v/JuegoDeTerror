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

    static ScrollInventory inventory;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<ScrollInventory>();
        }
    }

    public override void interact()
    {
        Item key = new Item();

        key.setKey(name);

        inventory.AddItem(key);
    }
}
