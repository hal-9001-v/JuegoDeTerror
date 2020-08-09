using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    key,
    note
}

public class Collectable : MonoBehaviour
{
    public string firstPageText;
    public string secondPageText;
    public Type currentType;
}
