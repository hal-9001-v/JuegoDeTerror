using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public string itemName;
    public Texture itemIcon;

    public string name { get; private set; } 

    public string GetItemName()
    {
        return itemName;
    }

    public Texture GetItemIcon()
    {
        return itemIcon;
    }

}