using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    static KeyTaker keyTaker;

    private void Awake()
    {
        if (keyTaker == null)
            keyTaker = FindObjectOfType<KeyTaker>();
    }

    public override void interact()
    {
        Debug.Log("HOI");
        keyTaker.AddKey(this);
    }
}
