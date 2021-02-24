using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public enum SoundProfile
    {
        Laboratory,
        Storage, 
        Office,
        Corridor
    }

    public SoundProfile profile;


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

        if (myMapNode != null)
        {
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

    private void OnDrawGizmos()
    {

        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();


        foreach (BoxCollider collider in colliders)
        {
            Handles.color = Color.blue;
            Handles.Label(collider.bounds.center, name+": "+weight.ToString());


            if (Selection.Contains(gameObject))
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
            }
            else
            {
                if (colliders.Length > 1)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
                }

            }


        }


    }

}
