using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public RoomMap myRoomMap;

    public Color safeColor;
    public Color nearbyColor;
    public Color dangerColor;

    // Start is called before the first frame update
    void Start()
    {
        myRoomMap = new RoomMap();
    }

}
