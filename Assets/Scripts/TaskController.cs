using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    public static TaskController instance;

    private TextMeshProUGUI textMesh;

    public Task[] tasks;
    int taskIndex;

    int safeTaskIndex;

    SaveManager mySaveManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            textMesh = FindObjectOfType<CanvasManager>().taskText;

            mySaveManager = FindObjectOfType<SaveManager>();

            if (tasks.Length != 0)
            {
                taskIndex = -1;
                startTask(0);
            }

        }
        else
        {
            Destroy(this);
        }

    }

    public void startTask(int i)
    {
        Debug.Log("Start Task");
        if (i >= 0 && i < tasks.Length
            && tasks[i] != null)
        {

            if (taskIndex >= 0 && taskIndex < tasks.Length && tasks[taskIndex] != null)
            {
                tasks[taskIndex].doneEvent.Invoke();
            }

            setSafeTask(taskIndex);
            taskIndex = i;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            textMesh.text = tasks[taskIndex].name;

            Debug.Log(tasks[taskIndex].name);

            tasks[taskIndex].startEvent.Invoke();
        }

    }

    public void startNextTask()
    {
        int i = taskIndex;
        i++;

        Debug.Log("Start Next Task");
        if (i >= 0 && i < tasks.Length
            && tasks[i] != null)
        {

            if (tasks[taskIndex] != null)
            {
                tasks[taskIndex].doneEvent.Invoke();
            }

            setSafeTask(taskIndex);
            taskIndex = i;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            textMesh.text = tasks[taskIndex].name;

            Debug.Log(tasks[taskIndex].name);

            tasks[taskIndex].startEvent.Invoke();
        }

    }



    void setSafeTask(int safe)
    {
        safeTaskIndex = safe;

        mySaveManager.saveGame();
    }

    public void loadData(GameData myData)
    {
        if (tasks.Length > myData.safeTask)
        {
            
            for (taskIndex = 0; taskIndex < myData.safeTask; taskIndex++)
            {
                Debug.Log("HEY BITCHU: " + tasks[taskIndex].name);
                //Go to next Task
                tasks[taskIndex].startEvent.Invoke();
                tasks[taskIndex].doneEvent.Invoke();

            }

            Debug.Log("Starting: " + tasks[taskIndex].name);
            safeTaskIndex = taskIndex;

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
        myData.safeTask = safeTaskIndex;

    }
}
