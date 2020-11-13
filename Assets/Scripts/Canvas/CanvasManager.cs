using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public Canvas noteCanvas;
    public Canvas inGameCanvas;
    public Canvas pauseCanvas;
    public Canvas startMenuCanvas;

    public TextMeshProUGUI taskText;

    GameManager gameManager;
    public void Start()
    {
        gameManager = GameManager.sharedInstance;
    }
    public void setPauseCanvas()
    {
        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = true;

        if (startMenuCanvas != null)
            startMenuCanvas.enabled = false;
    }

    public void setInGameCanvas()
    {
        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = true;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;

        if (startMenuCanvas != null)
            startMenuCanvas.enabled = false;
    }

    public void setNoteCanvas()
    {

        if (noteCanvas != null)
            noteCanvas.enabled = true;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;

        if (startMenuCanvas != null)
            startMenuCanvas.enabled = false;
    }

    public void setDeathCanvas()
    {

        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;


        if (startMenuCanvas != null)
            startMenuCanvas.enabled = false;
    }

    public void setStartMenuCanvas()
    {

        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;

        if (startMenuCanvas != null)
            startMenuCanvas.enabled = true;
    }

    public void resumeGame()
    {
        if (gameManager != null)
        {
            gameManager.resumeGame();
        }
        else
        {
            gameManager = GameManager.sharedInstance;

            if (gameManager != null)
            {
                gameManager.resumeGame();
            }
        }
    }

    public void goToStartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name, LoadSceneMode.Single);
    }
}
