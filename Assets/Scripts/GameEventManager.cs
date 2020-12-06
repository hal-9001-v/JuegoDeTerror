using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Q: HOW THIS CLASS WORKS?
 * 
 *R: This class represent the main and one subject.
 * Every single class that makes any reference to this one will meant that they where subscribing to any event (They will be listeners
 * following the Observer pattern)
 * 
 *Q: Want to create a new Event?
 * 
 *R: Create an event variable, and then its own public method.
 *
 *Q: Want to add some parameters to your events?
 *
 *R: If so, create a new class which inherits from EventArgs (only if you use EventHandler events) and define there your own parameters
 */

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