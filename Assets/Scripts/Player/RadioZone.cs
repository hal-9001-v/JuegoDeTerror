using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RadioZone : MonoBehaviour
{

    [TextArea(0, 4)]
    public string[] Text;

    public float delay;

}
