using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


/*
Needed elements on Scene:
-Room Components on Room GameObjects(Rooms must be linked as neighbours in order to work)

-Player Tracker Component on Player

-Enviromental Manager Component on Singleton GameObject
-CutsceneController Component on Singleton GameObject

 */
public class Pursuer : MonoBehaviour
{
    public static Pursuer instance { get; private set; }

    PlayerTracker player;
    EnviromentManager myEM;
    CutsceneController myCutsceneController;

    public InactiveState myInactiveState;
    public PursueIdleState myPursueIdleState;
    public RandomIdleState myRandomIdleState;

    public KillPlayerState myKillPlayerState;
    public DecideActionState myDecideActionState;

    public bool torchIsLit;

    public bool isKilling { get; set; }

    public enum pursuerStates
    {
        Inactive = 0,
        Patrol = 1,
        Pursue = 2
    }

    public enum PursuerLoadSpawn
    {
        SaveData,
        RandomInRange
    }

    [Header("Timers")]
    //Time Waiting on every room
    public float patrolTime = 2;
    public float pursueTime = 1;
    public float timeForKill = 2;

    [Space(10)]
    [Header("Settings")]
    public int spottingDistance = 2;
    public int torchBoost = 1;

    [Space(10)]
    [Header("Information")]
    public Room currentRoom;
    [Tooltip("0 => Inactive, 1 => Patrol randomly, 2 => Pursue player")]
    public int currentState;
    [Space(10)]
    [Header("Behaviour")]
    public PursuerLoadSpawn loadSpawn;
    public int spawnDistance = 3;

    [Range(0, 5)]
    public int vaultRooms;


    public Stack<Room> rooms;

    private void Awake()
    {
        if (instance == null)
        {
            initialize();
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
        //myEM.setAllRoomsSafe();
        myInactiveState.enter();


    }

    private void initialize()
    {
        player = FindObjectOfType<PlayerTracker>();

        myEM = FindObjectOfType<EnviromentManager>();
        if (myEM == null)
        {
            Debug.LogWarning("No Enviromental Manager in Scene!");
        }

        myCutsceneController = FindObjectOfType<CutsceneController>();
        if (myCutsceneController == null)
        {
            Debug.LogWarning("No cutscene controller in Scene!");
        }

        myInactiveState = new InactiveState(this);
        myPursueIdleState = new PursueIdleState(this, pursueTime);

        myRandomIdleState = new RandomIdleState(this, patrolTime);

        myKillPlayerState = new KillPlayerState(this, timeForKill);

        myDecideActionState = new DecideActionState(this);

    }

    public void killPlayer()
    {
        Debug.Log("Kill Player");
        myCutsceneController.killPlayer();
    }

    public Room moveToNextRoom()
    {
        currentRoom.setSafeRoom();

        foreach (Room r in currentRoom.neighbourRooms)
        {
            r.setSafeRoom();
        }

        currentRoom = rooms.Pop();

        currentRoom.setDangerColor();

        foreach (Room r in currentRoom.neighbourRooms)
        {
            r.setNearbyRoom();
        }

        //Move to Room(Just for debugging)
        transform.position = currentRoom.transform.position;

        return currentRoom;
    }

    public void startPursuing(Room startingRoom)
    {
        if (player.currentRoom != null)
        {
            myEM.setVaultRooms(vaultRooms);
            StopAllCoroutines();

            if (player.currentRoom != null)
            {
                //Stack the path to player
                rooms = myEM.myRoomMap.getPath(startingRoom, player.currentRoom);

            }
            else
            {
                startPatrol(currentRoom);
                return;
            }



            currentRoom = startingRoom;

            myPursueIdleState.enter();
            Debug.LogWarning("PURSUER: Start Pursuing");
        }
        else
        {
            Debug.LogWarning("Player is in no room");
        }

    }

    public void startPatrol(Room startingRoom)
    {
        myEM.setVaultRooms(vaultRooms);
        StopAllCoroutines();

        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(startingRoom, myEM.myRoomMap.getRandomRoom());
        currentRoom = startingRoom;

        myRandomIdleState.enter();
        Debug.Log("PURSUER: Start Patrol");
    }

    public void startIdle()
    {
        StopAllCoroutines();

        foreach (Room r in myEM.myRoomMap.roomList)
        {
            r.setSafeRoom();
        }

        myInactiveState.enter();
        Debug.Log("PURSUER: Start Idle");

    }

    public void updatePursuing()
    {
        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(currentRoom, myEM.myRoomMap.getRandomRoom());
    }

    public int getDistanceToPlayer()
    {

        int distance;

        if (myEM != null)
        {
            distance = myEM.myRoomMap.getDistance(currentRoom, player.currentRoom);

            if (torchIsLit)
            {
                if (distance > spottingDistance)
                {
                    distance -= torchBoost;
                }
            }
        }
        else
        {
            distance = -1;
        }

        return distance;
    }

    public PursuerData getSaveData()
    {
        return new PursuerData(currentRoom, currentState);
    }

    public void loadData(PursuerData ps)
    {
        StopAllCoroutines();

        myEM.setAllRoomsSafe();

        switch (loadSpawn)
        {
            case PursuerLoadSpawn.SaveData:
                spawnFromSaveData(ps);
                break;

            case PursuerLoadSpawn.RandomInRange:
                spawnRandomInRange(spawnDistance);
                break;

        }



    }

    void spawnFromSaveData(PursuerData ps)
    {
        currentState = ps.currentState;

        if (rooms != null)
        {
            rooms.Clear();
        }

        bool roomFound = false;

        foreach (Room r in FindObjectsOfType<Room>())
        {
            if (r.name == ps.currentRoomName)
            {
                currentRoom = r;
                roomFound = true;
                break;
            }

        }


        switch (currentState)
        {

            case (int)pursuerStates.Inactive:
                myInactiveState.enter();
                break;


            case (int)pursuerStates.Patrol:

                if (roomFound)
                    myRandomIdleState.enter();
                else
                {
                    Debug.LogError("Room wasn't found");
                    myInactiveState.enter();
                }
                break;


            case (int)pursuerStates.Pursue:

                if (roomFound)
                    myPursueIdleState.enter();
                else
                {
                    Debug.LogError("Room wasn't found");
                    myInactiveState.enter();
                }
                break;

        }
    }

    void spawnRandomInRange(int range)
    {
        currentState = (int)pursuerStates.Patrol;

        if (rooms != null)
        {
            rooms.Clear();
        }

        bool[] checks = new bool[myEM.myRoomMap.roomList.Count];
        int i = 0;

        while (true)
        {
            i = Random.Range(0, checks.Length);

        restart:

            if (!checks[i])
            {
                if (myEM.myRoomMap.getDistance(currentRoom, myEM.myRoomMap.roomList[i]) >= spawnDistance)
                {
                    currentRoom = myEM.myRoomMap.roomList[i];
                    break;
                }
            }
            else
            {
                if (i == (checks.Length - 1))
                {
                    i = 0;
                }
                else
                {
                    i++;
                }

                goto restart;
            }
        }

        myRandomIdleState.enter();

    }
    public class InactiveState : State
    {
        Pursuer myPursuer;
        public InactiveState(Pursuer myPursuer)
        {
            this.myPursuer = myPursuer;

        }

        public void enter()
        {
            myPursuer.currentState = (int)pursuerStates.Inactive;

        }

        public void execute()
        {

        }

        public void exit()
        {

        }


    }

    public class PursueIdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;
        PursueMoveState myPursueMoveState;

        public PursueIdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            myPursueMoveState = new PursueMoveState(myPursuer);

        }

        public void enter()
        {
            timeCounter = 0;
            myPursuer.currentState = (int)pursuerStates.Pursue;
            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * myPursuer.currentRoom.weight);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            {
                if (myPursuer.rooms.Count != 0)
                {
                    myPursueMoveState.enter();
                }
                else
                {
                    myPursuer.myDecideActionState.enter();
                }

            }

        }
    }

    public class PursueMoveState : State
    {

        Pursuer myPursuer;

        public PursueMoveState(Pursuer pursuer)
        {
            myPursuer = pursuer;
        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            myPursuer.moveToNextRoom();
            exit();
        }

        public void exit()
        {
            myPursuer.myPursueIdleState.enter();

        }
    }

    public class RandomIdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;

        RandomMoveState myRandomMoveState;

        public RandomIdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            myRandomMoveState = new RandomMoveState(myPursuer);
        }

        public void enter()
        {
            myPursuer.currentState = (int)pursuerStates.Patrol;

            timeCounter = 0;

            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * myPursuer.currentRoom.weight);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            if (myPursuer.getDistanceToPlayer() <= myPursuer.spottingDistance)
            {
                myPursuer.startPursuing(myPursuer.currentRoom);
            }
            else
            {
                if (myPursuer.rooms.Count != 0)
                {
                    myRandomMoveState.enter();
                }
                else
                {
                    myPursuer.startPatrol(myPursuer.currentRoom);
                }

            }

        }

    }

    public class RandomMoveState : State
    {
        Pursuer myPursuer;

        public RandomMoveState(Pursuer pursuer)
        {
            myPursuer = pursuer;
        }

        public void enter()
        {

            execute();
        }

        public void execute()
        {
            myPursuer.moveToNextRoom();
            exit();
        }

        public void exit()
        {
            myPursuer.myRandomIdleState.enter();

        }
    }

    public class KillPlayerState : State
    {
        Pursuer myPursuer;
        float timeToKill;

        public KillPlayerState(Pursuer pursuer, float timeToKill)
        {
            myPursuer = pursuer;
            this.timeToKill = timeToKill;

        }

        public void enter()
        {
            myPursuer.isKilling = true;
            execute();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        private IEnumerator Execute()
        {

            yield return new WaitForSeconds(timeToKill * myPursuer.currentRoom.weight);

            exit();
        }

        public void exit()
        {
            if (myPursuer.currentRoom == myPursuer.player.currentRoom)
            {
                myPursuer.killPlayer();
                myPursuer.myInactiveState.enter();
            }
            else
            {
                myPursuer.myDecideActionState.enter();
            }

            myPursuer.isKilling = false;
        }
    }

    public class DecideActionState : State
    {
        Pursuer myPursuer;

        public DecideActionState(Pursuer myPursuer)
        {
            this.myPursuer = myPursuer;
        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            exit();
        }

        public void exit()
        {
            if (myPursuer.currentRoom == myPursuer.player.currentRoom)
            {
                myPursuer.myKillPlayerState.enter();
                return;
            }

            if (myPursuer.getDistanceToPlayer() < myPursuer.spottingDistance)
            {
                myPursuer.startPursuing(myPursuer.currentRoom);
                return;
            }
            else
            {
                myPursuer.startPatrol(myPursuer.currentRoom);
                return;
            }

        }
    }
    public interface State
    {
        void enter();

        void execute();

        void exit();
    }


    private void OnDrawGizmos()
    {
        if (currentRoom != null)
        {

            Gizmos.color = Color.red;
            foreach (BoxCollider b in currentRoom.GetComponentsInChildren<BoxCollider>())
            {
                Gizmos.DrawCube(b.bounds.center, b.size * 0.9f);
            }
        }

    }

}




