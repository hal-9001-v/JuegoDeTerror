using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class EventCollider : MonoBehaviour
{
    public UnityEvent atEnterEvent;
    public bool onlyOnce;

    bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyOnce && !done)
            if (other.tag == "player")
            {
                done = true;
                atEnterEvent.Invoke();

            }
    }
}
