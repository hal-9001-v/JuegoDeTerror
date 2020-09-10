using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public List<Room> neighbourRooms;

    public int distance;
    public int weight = 1;

    public bool visited;
    public Room previousRoom;

    public UnityEvent atEnter;
    public UnityEvent atExit;

    [SerializeField]
    public List<Light> lights;

    private Color safeColor;
    private Color nearbyColor;
    private Color dangerColor;

    private void Start()
    {
        dangerColor = FindObjectOfType<Game>().dangerColor;
        nearbyColor = FindObjectOfType<Game>().nearbyColor;
        safeColor = FindObjectOfType<Game>().safeColor;

        setSafeRoom();
    }

    public void setSafeRoom() {
        foreach (Light l in lights) {
            
            l.color = safeColor;
        }
    }

    public void setNearbyRoom() {
        foreach (Light l in lights)
        {
            l.color = nearbyColor;
        }
    }

    public void setDangerColor() {
        foreach (Light l in lights)
        {
            l.color = dangerColor;
        }
    }

    public void initialize() {
        lights = new List<Light>();
        neighbourRooms = new List<Room>();
    }

}
