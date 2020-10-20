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
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public Canvas noteCanvas;
    public Canvas displayCanvas;
    public Canvas inGameCanvas;
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas gameOverCanvas;

    public PlayerBrain myPlayerBrain;

    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
        LanguageController.LoadLanguagesFile("languagesTextFile.txt");
        DontDestroyOnLoad(this);

        if (myPlayerBrain == null)
        {
            myPlayerBrain = FindObjectOfType<PlayerBrain>();
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

    //Método que cambia a el estado menu
    public void BackToMenu()
    {
        //SetGameState(GameState.menu);
        Cursor.lockState = CursorLockMode.None;
    }

    //Método que cambia a el estado gameOver
    public void GameOver()
    {
        //SetGameState(GameState.gameOver);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetGameState(GameState newGameState)
    {
        if (inGameCanvas != null && noteCanvas != null && mainMenuCanvas != null && myPlayerBrain != null)
        {
            switch (newGameState)
            {
                case GameState.inGame:

                    //Mostranmos y ocultamos los canvas que toquen
                    inGameCanvas.enabled = true;
                    noteCanvas.enabled = false;
                    mainMenuCanvas.enabled = false;
                    displayCanvas.enabled = false;


                    myPlayerBrain.enablePlayer(true);

                    break;
                case GameState.menu:
                    //Mostranmos y ocultamos los canvas que toquen
                    noteCanvas.enabled = false;
                    inGameCanvas.enabled = false;
                    mainMenuCanvas.enabled = true;

                    myPlayerBrain.enablePlayer(true);
                    break;
                case GameState.gameOver:
                    //Mostramos y ocultamos los canvas que toquen
                    noteCanvas.enabled = false;
                    inGameCanvas.enabled = false;
                    mainMenuCanvas.enabled = false;

                    myPlayerBrain.enablePlayer(false);
                    break;
            }

            displayCanvas.enabled = false;

        }

        switch (newGameState)
        {
            case GameState.inGame:
                //Mostranmos y ocultamos los canvas que toquen
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.menu:
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.gameOver:

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
