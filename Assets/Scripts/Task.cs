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
        if (TaskController.instance.currentTask == this)
            if (nextTask != null)
            {
                Debug.Log(" You have to: " + nextTask.name);
                TaskController.instance.StartTask(nextTask);
            }
            else {
                Debug.Log("No more taks");
            }
    }

}
