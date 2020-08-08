using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public Room[] neighbourRooms;

    public int distance;
    public int weight = 1;

    public bool visited;
    public Room previousRoom;

    public UnityEvent atEnter;
    public UnityEvent atExit;



}
