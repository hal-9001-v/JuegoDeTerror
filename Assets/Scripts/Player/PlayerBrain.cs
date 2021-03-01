using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    PlayerControls myPlayerControls;
    PlayerComponent[] myPlayerComponents;
    StatsComponent[] myStatsComponents;

    PlayerMovement movement;

    SaveManager mySaveManager;

    PlayerMovement pm;

    private void Awake()
    {
        myPlayerControls = new PlayerControls();

        myPlayerComponents = FindObjectsOfType<PlayerComponent>();
        myStatsComponents = FindObjectsOfType<StatsComponent>();
        mySaveManager = FindObjectOfType<SaveManager>();

        pm = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        foreach (PlayerComponent component in myPlayerComponents)
        {
            component.setPlayerControls(myPlayerControls);
        }

        setStatsValues();
    }

    public void enablePlayer(bool b)
    {
        pm.enabled = b;

        foreach (PlayerComponent component in myPlayerComponents)
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

    public void setStatsValues()
    {
        StatsData stats = mySaveManager.loadStats();

        foreach (StatsComponent component in myStatsComponents)
        {
            component.setStats(stats);
        }
    }
}
