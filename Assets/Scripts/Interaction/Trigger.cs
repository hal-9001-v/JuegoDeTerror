using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool readyForInteraction;
    public bool done;
    public bool onlyOnce;

    public void loadData(TriggerData myData)
    {
        done = myData.triggerDone;
        readyForInteraction = myData.triggerReady;

    }

    public TriggerData getSaveData()
    {
        return new TriggerData(name, done, readyForInteraction);
    }

    public void setReadyForInteraction(bool b) {
        readyForInteraction = b;
    }

}
