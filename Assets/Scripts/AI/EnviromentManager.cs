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

    }

    // Start is called before the first frame update
    void Start()
    {
        myRoomMap = new RoomMap();
    }

}
