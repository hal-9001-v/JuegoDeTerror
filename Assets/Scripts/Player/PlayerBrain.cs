using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    PlayerControls myPlayerControls;

    private void Awake()
    {
        myPlayerControls = new PlayerControls();

        foreach (PlayerComponent component in GetComponentsInChildren<PlayerComponent>(true))
        {
            component.setPlayerControls(myPlayerControls);

            Debug.Log(component.GetType());
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
