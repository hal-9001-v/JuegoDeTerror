using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    PlayerControls myPlayerControls;
    PlayerComponent[] myComponents;

    private void Awake()
    {
        myPlayerControls = new PlayerControls();

        myComponents = FindObjectsOfType<PlayerComponent>();

        foreach (PlayerComponent component in myComponents)
        {
            component.setPlayerControls(myPlayerControls);
        }
    }

    public void enablePlayer(bool b)
    {
        foreach (PlayerComponent component in myComponents)
        {
            component.enabled = b;
        }
    }

    private void OnEnable()
    {
        myPlayerControls.Enable();
    }

    private void OnDisable()
    {
        myPlayerControls.Disable();
    }
}
