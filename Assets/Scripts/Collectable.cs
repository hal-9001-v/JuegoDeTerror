using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Type
{
    key,
    note
}

public class Collectable : MonoBehaviour
{
    [TextArea(0, 40)] public string firstPageText;
    [TextArea(0, 40)] public string secondPageText;
    public bool moreThanOnePage;
    public Type currentType;
}
