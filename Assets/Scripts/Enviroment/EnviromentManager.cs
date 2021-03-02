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
    public Color vaultColor;

    [Range(5, 120)]
    public float vaultDelay;

    Door[] doors;

    bool readyToSetNewSafe = true;

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

    private void Start()
    {
        setVaultRooms(2);

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
            d.setLock(false);
        }
    }

    public void setVaultRooms(int n) {
        if (readyToSetNewSafe) {
            StartCoroutine(setVault(n));
        }
    }

    IEnumerator setVault(int n) {
        readyToSetNewSafe = false;
        foreach (Room r in myRoomMap.roomList) {
            r.setVaultRoom(false);
        }

        var rooms = new Room[n];

        for (int i = 0; i < n; i++) { 
            rooms[i] = myRoomMap.roomList[Random.Range(0, myRoomMap.roomList.Count)];

        }

        foreach (Room room in rooms) {
            room.setVaultRoom(true);
            Debug.Log(room.name + " set as Vault");
        }
        

        

        yield return new WaitForSeconds(vaultDelay);

        readyToSetNewSafe = true;
    }
}
