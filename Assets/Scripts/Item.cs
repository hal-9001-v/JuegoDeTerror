using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public string itemName;
    public Texture itemIcon;

    public bool isKey;
    public string name { get; private set; } 

    public string GetItemName()
    {
        return this.itemName;
    }

    public Texture GetItemIcon()
    {
        return this.itemIcon;
    }

    public void setKey(string name) {
        isKey = true;

        this.name = name;


    }


}