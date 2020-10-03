using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas menuCanvas;
    public bool startEnabled;
    public AudioClip buttonSound;
    public AudioSource audioSource;
    private GameManager gameManager;

    private void Start()
    {
        menuCanvas.enabled = startEnabled;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                gameManager.SetGameState(GameState.menu);
                menuCanvas.enabled = true;
            }
        }
    }

    public void BackToGame()
    {
        if (gameManager.currentGameState == GameState.menu)
        {
            gameManager.SetGameState(GameState.inGame);
            menuCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void BackToMenu()
    {
        if (gameManager.currentGameState == GameState.menu)
        {
            SceneManager.LoadScene("MainMneu");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void NewGame()
    {
        if(gameManager.currentGameState == GameState.menu)
        {
            gameManager.SetGameState(GameState.inGame);
            SceneManager.LoadScene("Escenario");
        }
    }

    public void PlayButtonSound()
    {
        if (menuCanvas.enabled)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
