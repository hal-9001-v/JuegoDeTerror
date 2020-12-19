using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour
{
    static EnviromentManager instance;

    public RoomMap myRoomMap;

    public Color safeColor;
    public Color nearbyColor;
    public Color dangerColor;

    Door[] doors;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            myRoomMap = new RoomMap();
            doors = FindObjectsOfType<Door>();

        }
        else
        {
            Debug.Log("2 Enviromental Managers in scene: " + name + " " + instance.name);
            Destroy(this);
        }


    }

    public void setAllRoomsSafe()
    {
        foreach (Room r in myRoomMap.roomList)
        {
            r.setSafeRoom();
        }
    }


    public void closeAllDoors()
    {
        foreach (Door d in doors)
        {
            d.closeDoor();
        }
    }

    public void openAllDoors()
    {
        foreach (Door d in doors)
        {
            d.setLock(false);
            d.openDoor();
        }
    }

    public void unlockAllDoors() {
        foreach (Door d in doors) { 
            
        }
    }


}
