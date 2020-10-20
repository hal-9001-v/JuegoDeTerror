﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour
{
    static EnviromentManager instance;

    public RoomMap myRoomMap;

    public Color safeColor;
    public Color nearbyColor;
    public Color dangerColor;

    public bool startGame;

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

    public void setAllRoomsSafe() {
        foreach (Room r in myRoomMap.roomList) {
            r.setSafeRoom();
        }
    }

}
