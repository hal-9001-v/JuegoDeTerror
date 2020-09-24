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
    private bool pauseIsOpen = false;

    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
        LanguageController.LoadLanguagesFile("languagesTextFile.txt");
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetGameState(GameState.preLoad);
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
