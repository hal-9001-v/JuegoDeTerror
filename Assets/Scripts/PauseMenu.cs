using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    private GameManager gameManager;

    private void Start()
    {
        pauseCanvas.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {                gameManager.SetGameState(GameState.menu);
                pauseCanvas.enabled = true;
            }
        }
    }

    public void BackToGame()
    {        if(gameManager.currentGameState == GameState.menu)
        {
            gameManager.currentGameState = GameState.inGame;
            pauseCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void BackToMenu()
    {
        if (gameManager.currentGameState == GameState.menu)
        {
            SceneManager.LoadScene("MainMneu");
            Cursor.lockState = CursorLockMode.None;
            Destroy(gameManager.gameObject);
        }
    }
}
