using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

    public GameObject noteCanvas;
    public GameObject inGameCanvas;

    public GameObject pauseCanvas;

    public PlayerBrain myPlayerBrain;

    public static GameManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {

            sharedInstance = this;
            LanguageController.LoadLanguagesFile("languagesTextFile.txt");
            DontDestroyOnLoad(this);

            if (myPlayerBrain == null)
            {
                myPlayerBrain = FindObjectOfType<PlayerBrain>();
            }
        }
        else {
            Debug.LogWarning("More than one Game Manager in scene");
            Destroy(this);
        }


    }

    private void Start()
    {
        //SetGameState(GameState.preLoad);
        LanguageController.language = PlayerPrefs.GetInt("language");
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


        Time.timeScale = 0;

        //Mostranmos y ocultamos los canvas que toquen
        noteCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);

        myPlayerBrain.enablePlayer(false);

    }

    void resumeState()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;

        //Mostranmos y ocultamos los canvas que toquen
        inGameCanvas.SetActive(true);
        noteCanvas.SetActive(false);
        pauseCanvas.SetActive(false);

        myPlayerBrain.enablePlayer(true);

        myPlayerBrain.setStatsValues();

    }

    void endState()
    {
        //Mostramos y ocultamos los canvas que toquen
        noteCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);

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
