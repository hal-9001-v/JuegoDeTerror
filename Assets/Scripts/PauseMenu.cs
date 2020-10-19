using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : PlayerComponent
{
    public Canvas pauseCanvas;
    private GameManager gameManager;

    private void Start()
    {
        pauseCanvas.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    private void pausePressed()
    {
        if (gameManager.currentGameState == GameState.inGame)
        {
            gameManager.SetGameState(GameState.menu);
            pauseCanvas.enabled = true;

        }
        else if (gameManager.currentGameState == GameState.menu)
        {
            gameManager.currentGameState = GameState.inGame;
            pauseCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Pause.performed += ctx => pausePressed();

    }
}
