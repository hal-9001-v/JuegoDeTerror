using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    const int LOADING_SCENE = 1;
    const int MENU_SCENE = 0;
    public enum Language
    {
        English,
        Español
    }

    public Language selectedLanguage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
            SceneManager.LoadScene(LOADING_SCENE);
            StartCoroutine(asyncLoad(i));
        }
        
        else {
            instance.goToSceneAsynch(i);
        }
    }


    public void goToMenu()
    {
        SceneManager.LoadScene(MENU_SCENE);
    }


    public void goToMenuAsynch()
    {
        if (instance == this)
            StartCoroutine(asyncLoad(MENU_SCENE));
        else
            instance.goToMenuAsynch();
        
    }


    IEnumerator asyncLoad(int sceneId)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(2f);

        while (!operation.isDone) {

            yield return null;
        }
        operation.allowSceneActivation = true;

    }


    public void exitGame()
    {
        Application.Quit();

    }

    public void changeLanguage()
    {
        if (selectedLanguage == Language.English)
            selectedLanguage = Language.Español;
        else
            selectedLanguage = Language.English;


    }


}
