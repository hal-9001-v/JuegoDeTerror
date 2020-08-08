using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomMap
{
    List<Room> roomList;

    public Material defaultMaterial;

    public RoomMap()
    {
        roomList = new List<Room>();
        foreach (Room r in GameObject.FindObjectsOfType<Room>())
        {
            roomList.Add(r);
        }
    }

    public Stack<Room> getPath(Room origin, Room destination)
    {

        if (origin == null)
        {
            Debug.Log("Origin does not exist");
            return null;
        }

        if (destination == null)
        {
            Debug.Log("Destination does not exist");
            return null;
        }

        //A Queue to run over all rooms which are not visited. It will get sorted after every pop
        RoomPriorityQueue unvisitedRooms = new RoomPriorityQueue(roomList.ToArray());

        foreach (Room r in roomList)
        {
            //Non-set nodes will be valued as the max value
            r.distance = int.MaxValue;
            r.previousRoom = null;
            r.visited = false;

            if (defaultMaterial != null)
                r.GetComponent<MeshRenderer>().material = defaultMaterial;
        }

        //Setting to 0 the first node, it will be analysed first
        origin.distance = 0;

        Room currentRoom = unvisitedRooms.pop();

        while (!unvisitedRooms.isEmpty())
        {
            currentRoom.visited = true;

            //Run over every next Room
            foreach (Room neighbour in currentRoom.neighbourRooms)
            {
                if (neighbour.visited) continue;

                //Set as previous node the shortest
                if (neighbour.distance > (currentRoom.distance + currentRoom.weight))
                {
                    neighbour.distance = neighbour.weight + currentRoom.distance;
                    neighbour.previousRoom = currentRoom;
                }
            }

            //Get next node which is closer to the origin
            currentRoom = unvisitedRooms.pop();

            //If all rooms' weights are the same, it can be stopped here
            if (currentRoom == destination) break;
        }

        //Start running the nodes from the last one
        currentRoom = destination;

        Stack<Room> path = new Stack<Room>();

        while (currentRoom != null)
        {
            path.Push(currentRoom);
            currentRoom = currentRoom.previousRoom;
        }

        return path;

    }

    public Room getRandomRoom() {
        return roomList[Random.Range(0,roomList.Count)];
    }

    class RoomPriorityQueue
    {

        List<Room> data;

        public RoomPriorityQueue(Room[] rooms)
        {
            data = new List<Room>();

            foreach (Room r in rooms)
            {
                data.Add(r);
            }

            data = data.OrderBy(i => i.distance).ToList();

        }

        public RoomPriorityQueue()
        {
            data = new List<Room>();

        }

        public Room pop()
        {
            data = data.OrderBy(i => i.distance).ToList();

            Room r = data.First();
            data.RemoveAt(0);

            return r;
        }

        public void push(Room r)
        {
            data.Add(r);
        }

        public bool isEmpty()
        {
            return data.Count == 0;
        }


    }

}
