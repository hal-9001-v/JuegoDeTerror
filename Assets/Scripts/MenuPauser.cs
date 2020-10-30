using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauser : PlayerComponent
{
    private GameManager gameManager;

    bool pause = false;

    private void Start()
    {
        gameManager = GameManager.sharedInstance;
    }

    public void setPause()
    {

        if (pause)
        {
            pause = false;
            gameManager.resumeGame();
            Debug.Log("Resume Game");

        }

        else
        {
            pause = true;
            gameManager.pauseGame();
            Debug.Log("Pause Game");
        }



    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.Pause.performed += ctx => setPause();

    }
}
