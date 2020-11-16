using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    public static TaskController instance;

    private TextMeshProUGUI textMesh;

    public Task firstTask;
    public Task currentTask { get; private set; }
    public Task safeTask { get; private set; }

    SaveManager mySaveManager;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            initialize();

            mySaveManager = FindObjectOfType<SaveManager>();

        }
        else
        {
            Destroy(this);
        }

    }

    private void initialize()
    {
        textMesh = FindObjectOfType<CanvasManager>().taskText;

        setCurrentTask(firstTask);

        safeTask = currentTask;
    }

    public void StartTask(Task task)
    {
        Debug.Log("Start Task");
        if (task == currentTask.nextTask)
        {
            setCurrentTask(task);

            setSafeTask(safeTask.nextTask);
        }
        else
        {
            Debug.LogWarning("Tried to start incorrect task on sequence: " + task.name + "!");
        }

    }

    void setCurrentTask(Task newTask)
    {
        if (newTask != null)
        {
            if (currentTask != null)
            {
                currentTask.doneEvent.Invoke();
            }

            currentTask = newTask;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            textMesh.text = currentTask.name;

            currentTask.startEvent.Invoke();
        }

    }

    void setSafeTask(Task task)
    {
        safeTask = task;

        mySaveManager.SaveGame();
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
