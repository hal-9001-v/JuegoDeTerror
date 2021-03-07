using Unity;
using UnityEngine;

public class MenuPauser : PlayerComponent
{
    private GameManager gameManager;
    public override void setPlayerControls(PlayerControls pc)
    {

        gameManager = GameManager.sharedInstance;

        if (gameManager != null)
        {
            pc.Normal.Pause.performed += ctx => gameManager.switchPause();

        }

        

    }
}
