using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{

    /*
     * 
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

    public void addToInventory()
    {
        if (inventory != null)
        {
            //inventory.AddItem(this);
            GameEventManager.sharedInstance.AddedItemToInventoryEvent(this);
        }
        else {
            Debug.LogWarning("No Inventory in scene!");
        }
        
    }
}
