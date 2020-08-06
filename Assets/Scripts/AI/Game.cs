using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    RoomMap myRoomMap;
    public int x;
    public int y;
    public int offset;
    public GameObject mapPrefab;

    public Room origin;
    public Room destination;

    public bool call;

    public Material myMaterial;
    public Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myRoomMap = new RoomMap();
        myRoomMap.defaultMaterial = defaultMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        if (call)
        {
            Stack<Room> myRooms = myRoomMap.getPath(origin, destination);

            foreach (Room r in myRooms)
            {
                r.GetComponent<MeshRenderer>().material = myMaterial;

            }

            call = false;


        }

    }
}
