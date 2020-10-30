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

    private void Start()
    {
        menuCanvas.enabled = startEnabled;
    }

    public void BackToGame()
    {
        menuCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMneu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Escenario");
    }

    public void PlayButtonSound()
    {
        if (menuCanvas.enabled)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }
}
