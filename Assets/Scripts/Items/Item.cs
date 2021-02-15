using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{

    public string itemName;
    public Texture itemIcon;
    public AudioClip sound;
   
    public void addToInventory()
    {
        //inventory.AddItem(this);
        GameEventManager.sharedInstance.AddedItemToInventoryEvent(this);

    }

    public abstract void useItem();
    
}