using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : MonoBehaviour
{
    PlayerTracker player;
    Game game;

    public Stack<Room> rooms;
    public Room currentRoom;

    public InactiveState myInactiveState;
    public IdleState myIdleState;
    public MoveState myMoveState;

    public RandomIdleState myRandomIdleState;
    public RandomMoveState myRandomMoveState;

    //Time Waiting on every room
    public float waitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerTracker>();
        game = FindObjectOfType<Game>();

        myInactiveState = new InactiveState(this);
        myIdleState = new IdleState(this, waitTime);
        myMoveState = new MoveState(this);

        myRandomIdleState = new RandomIdleState(this, waitTime);
        myRandomMoveState = new RandomMoveState(this);


        myInactiveState.enter();
    }

    public Room moveToNextRoom()
    {
        currentRoom = rooms.Pop();

        //Move to Room(Just for debugging)
        Vector3 aux = currentRoom.transform.position;
        aux.y = transform.position.y;

        aux.z -= 10;
        aux.x += 10;

        transform.position = aux;

        return currentRoom;
    }

    public void startPursuing(Room startingRoom)
    {
        //Stack the path to player
        rooms = game.myRoomMap.getPath(currentRoom, player.currentRoom);
        currentRoom = startingRoom;

        myIdleState.enter();
        Debug.Log("PURSUER: Start Pursuing");
    }

    public void startPatrol(Room startingRoom)
    {

        //Stack the path to player
        rooms = game.myRoomMap.getPath(currentRoom, game.myRoomMap.getRandomRoom());
        currentRoom = startingRoom;

        myRandomIdleState.enter();
        Debug.Log("PURSUER: Start Patrol");
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

    public class IdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;

        public IdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

        }

        public void enter()
        {
            timeCounter = 0;
            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime);

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
                    myPursuer.myMoveState.enter();
                }
                else
                {
                    if (myPursuer.game.myRoomMap.getPath(myPursuer.currentRoom, myPursuer.player.currentRoom).Count <= 1)
                    {
                        myPursuer.startPursuing(myPursuer.currentRoom);
                    }
                    else
                    {
                        myPursuer.startPatrol(myPursuer.currentRoom);
                    }


                }

            }

        }
    }
    public class MoveState : State
    {

        Pursuer myPursuer;

        public MoveState(Pursuer pursuer)
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

            if (myPursuer.currentRoom == myPursuer.player.currentRoom)
            {
                Debug.Log("Kill Player");

                myPursuer.myInactiveState.enter();

            }
            else
            {
                myPursuer.myIdleState.enter();
            }

        }
    }

    public class RandomIdleState : State
    {
        public float holdingTime;
        public float timeCounter;

        Pursuer myPursuer;

        public RandomIdleState(Pursuer myPursuer, float holdingTime)
        {
            this.myPursuer = myPursuer;
            this.holdingTime = holdingTime;

        }

        public void enter()
        {
            timeCounter = 0;
            execute();
        }

        private IEnumerator Execute()
        {
            yield return new WaitForSeconds(holdingTime);

            exit();
        }

        public void execute()
        {
            myPursuer.StartCoroutine(Execute());
        }

        public void exit()
        {
            if (myPursuer.game.myRoomMap.getPath(myPursuer.currentRoom, myPursuer.player.currentRoom).Count <= 2)
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



    public interface State
    {
        void enter();

        void execute();

        void exit();
    }
}




