using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    PlayerTracker player;
    EnviromentManager myEM;
    CutsceneController myCutsceneController;

    public Stack<Room> rooms;
    public Room currentRoom;

    public InactiveState myInactiveState;
    public PursueIdleState myPursueIdleState;
    public PursueMoveState myPursueMoveState;

    public RandomIdleState myRandomIdleState;
    public RandomMoveState myRandomMoveState;

    public KillPlayerState myKillPlayerState;
    public DecideActionState myDecideActionState;

    //Time Waiting on every room
    public float patrolTime = 2;
    public float pursueTime = 1;
    public float timeForKill = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerTracker>();
        myEM = FindObjectOfType<EnviromentManager>();
        myCutsceneController = FindObjectOfType<CutsceneController>();

        myInactiveState = new InactiveState(this);
        myPursueIdleState = new PursueIdleState(this, pursueTime, currentRoom);
        myPursueMoveState = new PursueMoveState(this);

        myRandomIdleState = new RandomIdleState(this, patrolTime, currentRoom);
        myRandomMoveState = new RandomMoveState(this);

        myKillPlayerState = new KillPlayerState(this, timeForKill, currentRoom);

        myDecideActionState = new DecideActionState(this);


        myInactiveState.enter();
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

        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(startingRoom, player.currentRoom);
        currentRoom = startingRoom;

        myPursueIdleState.enter();
        //Debug.LogWarning("PURSUER: Start Pursuing");
    }

    public void startPatrol(Room startingRoom)
    {

        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(startingRoom, myEM.myRoomMap.getRandomRoom());
        currentRoom = startingRoom;

        myRandomIdleState.enter();
        //Debug.Log("PURSUER: Start Patrol");
    }

    public void updatePursuing()
    {
        //Stack the path to player
        rooms = myEM.myRoomMap.getPath(currentRoom, myEM.myRoomMap.getRandomRoom());
    }

    public int distanceToPlayer()
    {
        return myEM.myRoomMap.getPath(currentRoom, player.currentRoom).Count;
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

        Room currentRoom;

        Pursuer myPursuer;

        public PursueIdleState(Pursuer myPursuer, float holdingTime, Room currentRoom)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            this.currentRoom = currentRoom;

        }

        public void enter()
        {
            timeCounter = 0;
            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * currentRoom.weight);

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
                    myPursuer.myPursueMoveState.enter();
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

        Room currentRoom;

        Pursuer myPursuer;

        public RandomIdleState(Pursuer myPursuer, float holdingTime, Room currentRoom)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

            this.currentRoom = currentRoom;

        }

        public void enter()
        {
            timeCounter = 0;

            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime * currentRoom.weight);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            if (myPursuer.distanceToPlayer() <= 3)
            {
                myPursuer.startPursuing(myPursuer.currentRoom);
            }
            else
            {
                if (myPursuer.rooms.Count != 0)
                {
                    myPursuer.myRandomMoveState.enter();
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

        Room currentRoom;

        public KillPlayerState(Pursuer pursuer, float timeToKill, Room currentRoom)
        {
            myPursuer = pursuer;
            this.timeToKill = timeToKill;

            this.currentRoom = currentRoom;
        }

        public void enter()
        {
            execute();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        private IEnumerator Execute()
        {

            yield return new WaitForSeconds(timeToKill * currentRoom.weight);

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

            if (myPursuer.distanceToPlayer() < 4)
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
}




