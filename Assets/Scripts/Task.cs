using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public int taskNumber;

    public void goToNextTask()
    {
        TaskController.instance.startNextTask();
    }

}
