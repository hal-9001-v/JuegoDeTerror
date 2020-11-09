using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas noteCanvas;
    public Canvas inGameCanvas;
    public Canvas pauseCanvas;

    public void setPause()
    {
        if (noteCanvas != null)
            noteCanvas.enabled = false;
        
        if (inGameCanvas != null)
            inGameCanvas.enabled = false;
        
        if (pauseCanvas != null)
            pauseCanvas.enabled = true;
    }

    public void setInGame()
    {
        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = true;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;
    }

    public void setNote()
    {

        if (noteCanvas != null)
            noteCanvas.enabled = true;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;
    }

    public void setDeath()
    {

        if (noteCanvas != null)
            noteCanvas.enabled = false;

        if (inGameCanvas != null)
            inGameCanvas.enabled = false;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;
    }


}
