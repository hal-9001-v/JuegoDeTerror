using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent doneEvent;

    public int taskNumber;

    public Task nextTask;

    public void goToNextTask()
    {
        if (nextTask != null)
        {
            TaskController.instance.StartTask(nextTask);
        }
        else
        {
            Debug.Log("No more taks");
        }
    }

}
