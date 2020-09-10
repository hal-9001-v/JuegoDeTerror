using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public void SetOpenObject(bool open)
    {
        this.GetComponent<HighlightedObject>().SetOpenObject(open);
    }
}
