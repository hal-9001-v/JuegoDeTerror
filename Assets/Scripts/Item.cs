using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public static Item sharedInstance;

    public List<string> itemNames;
    public List<Texture> itemIcons;

    private void Awake()
    {
        sharedInstance = this;
    }

    public string GetItemName(int index)
    {
        return this.itemNames[index];
    }

    public Texture GetItemIcon(int index)
    {
        return this.itemIcons[index];
    }
}