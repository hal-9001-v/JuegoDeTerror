using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public List<Room> neighbourRooms;

    public float distance;
    public float weight = 1;

    public bool visited;
    public Room previousRoom;

    public UnityEvent atEnter;
    public UnityEvent atExit;

    [SerializeField]
    public List<Light> lights;

    private Color safeColor;
    private Color nearbyColor;
    private Color dangerColor;

    public MapNode myMapNode;

    private void Awake()
    {
        EnviromentManager myEM = FindObjectOfType<EnviromentManager>();

        if (myEM != null)
        {
            dangerColor = myEM.dangerColor;
            nearbyColor = myEM.nearbyColor;
            safeColor = myEM.safeColor;
        }
        else
        {
            Debug.LogWarning("No Enviroment Manager in Scene");
        }

        if (myMapNode == null)
        {
            myMapNode = GetComponentInChildren<MapNode>();
        }
    }

    private void Start()
    {
        setSafeRoom();
    }

    public void setSafeRoom()
    {
        foreach (Light l in lights)
        {
            l.color = safeColor;
        }

        if (myMapNode != null) {
            myMapNode.setSafe();
        }

    }

    public void setNearbyRoom()
    {
        foreach (Light l in lights)
        {
            l.color = nearbyColor;
        }

        if (myMapNode != null)
        {
            myMapNode.setNearby();
        }
    }

    public void setDangerColor()
    {
        foreach (Light l in lights)
        {
            l.color = dangerColor;
        }


        if (myMapNode != null)
        {
            myMapNode.setDanger();
        }
    }

    public void initialize()
    {
        lights = new List<Light>();
        neighbourRooms = new List<Room>();
    }

}
