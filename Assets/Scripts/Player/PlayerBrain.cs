using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    PlayerControls myPlayerControls;
    PlayerComponent[] myPlayerComponents;
    StatsComponent[] myStatsComponents;

    private void Awake()
    {
        myPlayerControls = new PlayerControls();
    }

    private void Start()
    {
        myPlayerComponents = FindObjectsOfType<PlayerComponent>();
        myStatsComponents = FindObjectsOfType<StatsComponent>();


        foreach (PlayerComponent component in myPlayerComponents)
        {
            component.setPlayerControls(myPlayerControls);
        }

        setStatsValues();
    }

    public void enablePlayer(bool b)
    {
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
        StatsData stats = SaveManager.loadStats();

        foreach (StatsComponent component in myStatsComponents)
        {
            component.setStats(stats);
        }
    }
}
