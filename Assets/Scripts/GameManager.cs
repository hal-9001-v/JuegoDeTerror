using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

//Posibles Estados de Juego
public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public Canvas noteCanvas;
    public Canvas inGameCanvas;
    public Canvas mainMenuCanvas;
    public Canvas gameOverCanvas;

    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
        LanguageController.LoadLanguagesFile("languagesTextFile.txt");
    }

    private void Start()
    {
        if (currentGameState == GameState.menu)
            BackToMenu();
    }

    //Método que cambia a el estado inGame
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Método que cambia a el estado menu
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
        Cursor.lockState = CursorLockMode.None;
    }

    //Método que cambia a el estado gameOver
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.inGame:
                //Mostranmos y ocultamos los canvas que toquen
                inGameCanvas.enabled = true;
                noteCanvas.enabled = false;
                mainMenuCanvas.enabled = false;
                break;
            case GameState.menu:
                //Mostranmos y ocultamos los canvas que toquen
                noteCanvas.enabled = false;
                inGameCanvas.enabled = false;
                mainMenuCanvas.enabled = true;
                break;
            case GameState.gameOver:
                //Mostranmos y ocultamos los canvas que toquen
                noteCanvas.enabled = false;
                inGameCanvas.enabled = false;
                mainMenuCanvas.enabled = false;
                break;
        }

        currentGameState = newGameState;
    }

}
