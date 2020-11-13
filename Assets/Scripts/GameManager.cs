using UnityEngine;
using UnityEngine.SceneManagement;

//Posibles Estados de Juego
public enum GameState
{
    preLoad,
    pause,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public CanvasManager myCanvasManager;

    public PlayerBrain myPlayerBrain;

    public static GameManager sharedInstance;

    bool paused;

    private void Awake()
    {
        if (sharedInstance == null)
        {

            sharedInstance = this;
            LanguageController.LoadLanguagesFile("languagesTextFile.txt");

            if (myPlayerBrain == null)
            {
                myPlayerBrain = FindObjectOfType<PlayerBrain>();
            }
        }
        else
        {
            Debug.LogWarning("More than one Game Manager in scene");
            Destroy(this);
        }

        myCanvasManager = FindObjectOfType<CanvasManager>();


    }

    private void Start()
    {
        //SetGameState(GameState.preLoad);
        LanguageController.language = PlayerPrefs.GetInt("language");

        setGameState(currentGameState);
    }

    //Método que cambia a el estado inGame
    public void StartGame()
    {
        //SetGameState(GameState.inGame);
        SceneManager.LoadScene("SampleScene");
        currentGameState = GameState.inGame;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void pauseState()
    {
        Cursor.lockState = CursorLockMode.None;

        paused = true;

        Time.timeScale = 0;

        if (myCanvasManager != null)
        {
            myCanvasManager.setPauseCanvas();
        }

        if (myPlayerBrain != null)
            myPlayerBrain.enablePlayer(false);

    }
    
    public void switchPause() {

        if (paused)
        {
            resumeGame();

        }
        else {
            pauseGame();
        }

    }

    void resumeState()
    {
        paused = false;

        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;

        if (myCanvasManager != null)
        {
            myCanvasManager.setInGameCanvas();
        }

        if (myPlayerBrain != null)
        {
            myPlayerBrain.enablePlayer(true);

            myPlayerBrain.setStatsValues();
        }


    }

    void endState()
    {
        if (myCanvasManager != null)
        {
            myCanvasManager.setDeathCanvas();
        }

        if (myPlayerBrain != null)
            myPlayerBrain.enablePlayer(false);

        Cursor.lockState = CursorLockMode.None;
    }

    //Método que cambia a el estado menu
    public void BackToMenu()
    {
        //SetGameState(GameState.menu);
        Cursor.lockState = CursorLockMode.None;
    }

    public void pauseGame()
    {
        setGameState(GameState.pause);
    }

    public void resumeGame()
    {
        setGameState(GameState.inGame);
    }

    //Método que cambia a el estado gameOver
    public void GameOver()
    {
        setGameState(GameState.gameOver);
    }

    void setGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.inGame:
                resumeState();
                break;

            case GameState.pause:
                pauseState();
                break;

            case GameState.gameOver:
                endState();
                break;

            case GameState.preLoad:
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }


        currentGameState = newGameState;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMneu");
    }
}
