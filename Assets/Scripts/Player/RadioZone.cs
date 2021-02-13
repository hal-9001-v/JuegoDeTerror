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
    public bool readyForInteraction;
    public bool onlyOnce;

    public bool done;

    Collider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    public RadioZoneData getSaveData() {
        return new RadioZoneData(name, done, readyForInteraction);
    }

    public void loadData(RadioZoneData data) {
        done = data.done;
        readyForInteraction = data.readyForInteraction;
    }

    public void setReadyForInteraction(bool b) {
        readyForInteraction = b;

        if (b)
            myCollider.enabled = true;
        else
            myCollider.enabled = false;

    }


}
