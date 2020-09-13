using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public PlayerData myPlayerData;

    public GameData()
    {

    }
}

[Serializable]
public class PlayerData
{
    public float[] playerPosition;

    public PlayerData(Vector3 position)
    {
        this.playerPosition[0] = position.x;
        this.playerPosition[1] = position.y;
        this.playerPosition[2] = position.z;
    }
}
