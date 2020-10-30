using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    public static TaskController instance;

    public TextMeshProUGUI textMesh;

    public Task firstTask;
    public Task currentTask { get; private set; }
    public Task safeTask { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            currentTask = firstTask;

            safeTask = currentTask;

            applyTaskEffect(currentTask);
        }
        else
        {
            Destroy(this);
        }

    }

    public void StartTask(Task task)
    {
        applyTaskEffect(task);

        setSafeTask(safeTask.nextTask);


    }

    void applyTaskEffect(Task task)
    {
        if (task != null)
        {
            if (currentTask != null)
            {
                currentTask.doneEvent.Invoke();
            }

            currentTask = task;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            textMesh.text = currentTask.name;

            currentTask.startEvent.Invoke();
        }

    }

    void setSafeTask(Task task)
    {
        safeTask = task;

        SaveManager.SaveGame();
    }

    public void loadData(GameData myData)
    {
        Task loopTask;

        loopTask = firstTask;

        int loadNumber = myData.safeTask;
        bool found = false;

        while (loopTask != null)
        {
            if (loopTask.taskNumber == loadNumber)
            {
                found = true;

                break;
            }

            loopTask = loopTask.nextTask;
        }

        if (found)
        {
            Debug.Log(loopTask.name);
            currentTask = loopTask;
            safeTask = loopTask;

            textMesh.text = currentTask.name;
            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);

        }
        else
        {
            Debug.LogError("Loaded Task doesn't exist in current Scene");
        }

    }

    public void saveData(GameData myData)
    {
        myData.safeTask = safeTask.taskNumber;
    }
}
