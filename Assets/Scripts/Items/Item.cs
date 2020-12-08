﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{

    public string itemName;
    public Texture itemIcon;
   
    public void addToInventory()
    {
        //inventory.AddItem(this);
        GameEventManager.sharedInstance.AddedItemToInventoryEvent(this);

        Debug.LogWarning("No Inventory in scene!");
    }

    public abstract void useItem();
    
}