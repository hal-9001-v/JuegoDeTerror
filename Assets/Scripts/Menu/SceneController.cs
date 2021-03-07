using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    
    const int menuScene = 0;
    const int loadingScene = 1;
    const int restartScene = 3;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Application.targetFrameRate = 60;

            DontDestroyOnLoad(this);
        }

        /*
         * It may be needed on a scene, so keep as destroyable
        else
        {
            Destroy(this);
        }
        */
    }

    public void goToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void goToNextSceneAsynch()
    {
        if (instance == this)
            StartCoroutine(asyncLoad(SceneManager.GetActiveScene().buildIndex + 1));
        else
            instance.goToNextSceneAsynch();
    }

    public void goToScene(int i) {
        SceneManager.LoadScene(i);
    }
    public void goToSceneAsynch(int i)
    {
        if (instance == this) {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SceneManager.LoadScene(loadingScene);
            StartCoroutine(asyncLoad(i));
        }
        
        else {
            instance.goToSceneAsynch(i);
        }
    }


    public void goToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }


    public void goToMenuAsynch()
    {
        if (instance == this)
            StartCoroutine(asyncLoad(restartScene));
        else
            instance.goToMenuAsynch();
        
    }


    IEnumerator asyncLoad(int sceneId)
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(loadingScene);

        yield return new WaitForSecondsRealtime(2);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        Debug.Log("Loading " + SceneManager.GetSceneByBuildIndex(sceneId).name + " scene");
        
        while (!operation.isDone) {

            Debug.Log("Progress: " + operation.progress*100+"%");

            yield return null;
        }

        Debug.Log("Done loading " + SceneManager.GetSceneByBuildIndex(sceneId).name + " scene!");        

    }


    public void exitGame()
    {
        Application.Quit();

    }


}
