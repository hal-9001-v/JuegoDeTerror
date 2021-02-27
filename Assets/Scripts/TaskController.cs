using UnityEngine;
using TMPro;
using System.Collections;

public class TaskController : MonoBehaviour
{
    public static TaskController instance;

    private TextMeshProUGUI textMesh;

    public Task[] tasks;
    int taskIndex = -1;

    SaveManager mySaveManager;

    [Range(0.01f, 1)]
    public float displayDelay = 0.01f;

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
            if (taskIndex >= 0 && tasks[taskIndex] != null)
                tasks[taskIndex].doneEvent.Invoke();

            taskIndex = i;

            //taskCanvasText.text = LanguageController.GetTextInLanguage("Mission" + task.taskNumber);
            //textMesh.text = tasks[taskIndex].name;


            StartCoroutine(displayTask());

            Debug.Log("TASK: "+tasks[taskIndex].name);

            tasks[taskIndex].startEvent.Invoke();

            mySaveManager.saveGame();
        
        }

    }

    IEnumerator displayTask() {

        textMesh.text = tasks[taskIndex].name;
        textMesh.alpha = 0;

        for (int i = 0; i < 10; i++) {
            textMesh.alpha += 0.1f;

            yield return new WaitForSeconds(displayDelay);
        }

        yield return 0;
    }

    public void startNextTask()
    {
        startTask(taskIndex + 1);
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
