using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
    public UnityEvent atStartEvent;
    public UnityEvent atEndEvent;

    public int taskNumber;

    public Task nextTask;

    public MissionController myMissionController;

    public void goToNextTask() {
        if (nextTask != null) {
            myMissionController.StartMission(nextTask);
        }
    }

}
