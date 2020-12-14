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

    public Room A;
    public Room B;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        myRoomMap = new RoomMap();

    }

    private void Start()
    {
        Debug.Log(myRoomMap.getPath(A, B).Count);
    }

    public void setAllRoomsSafe() {
        foreach (Room r in myRoomMap.roomList) {
            r.setSafeRoom();
        }
    }

}
