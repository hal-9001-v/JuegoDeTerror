using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Room : MonoBehaviour
{
    public Room[] neighbourRooms;

    public int distance;
    public int weight = 1;

    public bool visited;
    public Room previousRoom;

}
