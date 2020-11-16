using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPauser : PlayerComponent
{
    private GameManager gameManager;

    bool pause = false;
    public override void setPlayerControls(PlayerControls pc)
    {
        gameManager = GameManager.sharedInstance;

        if (gameManager != null) {
            pc.Normal.Pause.performed += ctx => gameManager.switchPause();            

        }

    }
}
