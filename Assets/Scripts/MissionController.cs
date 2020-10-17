using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    private TextMeshProUGUI missionCanvasText;
    private int missionNumber = 1;
    public int totalMissions = 10;

    public Task firstTask;
    Task currentTask;

    private void Awake()
    {
        missionCanvasText = GetComponent<TextMeshProUGUI>();

        currentTask = firstTask;
    }



    public void StartMission(Task task)
    {
        if (currentTask != null) {
            currentTask.atEndEvent.Invoke();
        }
        currentTask = task;

        missionCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
        if (missionNumber < totalMissions)
        {
            missionNumber = task.taskNumber;
        }

        currentTask.atStartEvent.Invoke();
    }
}
