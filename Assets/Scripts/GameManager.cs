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

    public static GameManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        StartGame();
    }

    //Método que cambia a el estado inGame
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    //Método que cambia a el estado menu
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    //Método que cambia a el estado gameOver
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void SetGameState(GameState newGameState)
    {
        switch(newGameState)
        {
            case GameState.inGame:
                //Mostranmos y ocultamos los canvas que toquen
                noteCanvas.enabled = false;
                break;
            case GameState.menu:
                //Mostranmos y ocultamos los canvas que toquen
                noteCanvas.enabled = false;
                break;
            case GameState.gameOver:
                //Mostranmos y ocultamos los canvas que toquen
                noteCanvas.enabled = false;
                break;
        }

        currentGameState = newGameState;
    }

}
