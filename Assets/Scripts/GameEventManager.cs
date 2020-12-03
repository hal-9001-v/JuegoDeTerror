using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager sharedInstance;

    public class OnUsedItemForInventory : EventArgs
    {
        public Item myItem;
    }

    private void Awake()
    {
        sharedInstance = this;
    }

    //////////////////////////////////////
    //Scroll Inventory Events
    //////////////////////////////////////

    public event EventHandler<OnUsedItemForInventory> OnAddedItemToInventory;
    public void AddedItemToInventoryEvent(Item newItem)
    {
        OnAddedItemToInventory?.Invoke(this, new OnUsedItemForInventory { myItem = newItem });
    }

    public event EventHandler<OnUsedItemForInventory> OnDeletedItemToInventory;
    public void DeletedItemToInventoryEvent(Item newItem)
    {
        OnDeletedItemToInventory?.Invoke(this, new OnUsedItemForInventory { myItem = newItem });
    }
}
