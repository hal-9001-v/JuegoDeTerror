using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RadioZone : MonoBehaviour
{

    [TextArea(0, 4)]
    public string[] Text;

    public float delay;

    [Space(2)]
    public UnityEvent startEvent;
    [Space(1)]
    public UnityEvent endEvent;
    
    [Space(2)]
    public bool onlyOnce;

    [HideInInspector]
    public bool done;


}
