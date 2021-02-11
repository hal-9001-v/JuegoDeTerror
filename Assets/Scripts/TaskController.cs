using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    public static TaskController instance;

    private TextMeshProUGUI textMesh;

    public Task[] tasks;
    int taskIndex = 0;

    SaveManager mySaveManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            textMesh = FindObjectOfType<CanvasManager>().taskText;

            mySaveManager = FindObjectOfType<SaveManager>();



        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        if (tasks.Length != 0)
        {
            startTask(0);
        }
    }

    void startTask(int i)
    {
        if (i >= 0 && i < tasks.Length && tasks[i] != null)
        {
            if (tasks[taskIndex] != null)
                tasks[taskIndex].doneEvent.Invoke();

            taskIndex = i;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            textMesh.text = tasks[taskIndex].name;

            Debug.Log(tasks[taskIndex].name);

            tasks[taskIndex].startEvent.Invoke();

            mySaveManager.saveGame();
        }

    }

    public void startNextTask()
    {
        int i = taskIndex;
        i++;

        startTask(i);
    }



    public void loadData(GameData myData)
    {
        if (tasks.Length > myData.safeTask)
        {
            taskIndex = myData.safeTask;

            Debug.Log("Starting: " + tasks[taskIndex].name);

            textMesh.text = tasks[taskIndex].name;
            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
        }
        else
        {
            Debug.LogError("Tasks Save Data Files not correct!");
        }

    }

    public void saveData(GameData myData)
    {
        myData.safeTask = taskIndex;
    }
}
