using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomTool : EditorWindow
{
    Room selectedRoom;

    bool block = false;

    int lightIndex = 0;
    int roomIndex = 0;

    GameObject roomGroup;


    [MenuItem("Tools/Room Tool")]
    public static void showWindow()
    {
        GetWindow(typeof(RoomTool));
    }

    private void OnGUI()
    {
        roomGroup = GameObject.FindGameObjectWithTag("RoomGroup");

        if (createRoom())
        {
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.Space();
        }

        if (getObjectFromSelection<Room>() != null && !block)
            selectedRoom = getObjectFromSelection<Room>();

        if (selectedRoom != null)
        {
            GUILayout.Label("Selected Room: " + selectedRoom.name, EditorStyles.boldLabel);
        }
        else
        {
            GUILayout.Label("Selected Room: NONE", EditorStyles.boldLabel);
            block = false;

            return;
        }

        if (block)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Unlock Room"))
            {
                block = false;
            }
            GUILayout.Label("LOCKED", EditorStyles.boldLabel); GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Lock Room"))
            {
                block = true;
            }
            GUILayout.Label("UNLOCKED", EditorStyles.boldLabel);

            GUILayout.EndHorizontal();

        }

        valuesMenu();

        lightMenu();

        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.Space();
        }

        roomMenu();


        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Removed Objects aren't eliminated from scene", MessageType.Warning);

        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.Space();
        }

        roomColliderMenu();


    }

    void valuesMenu() {
        GUILayout.Label("Weight", EditorStyles.boldLabel);
        selectedRoom.weight = EditorGUILayout.FloatField(selectedRoom.weight, GUILayout.Width(30));
        EditorUtility.SetDirty(selectedRoom);
        EditorGUILayout.Space();
    }

    private void lightMenu()
    {

        GUILayout.Label("LIGHTS", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Lights wich are affected by the enviroment", MessageType.Info);


        string listString = "";

        if (selectedRoom.lights.Count == 0)
        {
            listString += "No Lights in selected Room \n";
        }

        foreach (Light l in selectedRoom.lights)
        {
            if (l == null)
            {
                listString += "Empty \n";
            }
            else
            {
                listString += l.name + "\n";
            }
        }
        GUILayout.TextArea(listString, EditorStyles.linkLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Lights"))
        {
            List<Light> lightSelection = getObjectsFromSelection<Light>();

            foreach (Light l in lightSelection)
            {
                selectedRoom.lights.Add(l);
            }

            cleanList<Light>(selectedRoom.lights);
            EditorUtility.SetDirty(selectedRoom);

        }

        if (GUILayout.Button("Remove Lights"))
        {
            List<Light> lights = getObjectsFromSelection<Light>();

            foreach (Light l in lights)
            {
                selectedRoom.lights.Remove(l);
            }

            cleanList<Light>(selectedRoom.lights);
            EditorUtility.SetDirty(selectedRoom);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create Light"))
        {
            GameObject newLight = new GameObject();
            newLight.transform.position = selectedRoom.transform.position;
            newLight.name = "light";
            newLight.transform.SetParent(selectedRoom.transform);
            selectedRoom.lights.Add(newLight.AddComponent<Light>());

            Selection.activeObject = newLight;

            EditorUtility.SetDirty(selectedRoom);
        }

        string[] popUpOptions = new string[selectedRoom.lights.Count];

        for (int i = 0; i < popUpOptions.Length; i++)
        {
            if (selectedRoom.lights[i] != null)
                popUpOptions[i] = selectedRoom.lights[i].name;
            else
            {
                popUpOptions[i] = "Empty";
            }
        }

        EditorGUILayout.Space();

        GUILayout.Label("Remove by selection", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        lightIndex = EditorGUILayout.Popup(lightIndex, popUpOptions);

        if (GUILayout.Button("Remove"))
        {
            if (lightIndex < selectedRoom.lights.Count)
            {
                selectedRoom.lights.RemoveAt(lightIndex);
                EditorUtility.SetDirty(selectedRoom);
            }
        }
        GUILayout.EndHorizontal();


    }

    private void roomMenu()
    {
        GUILayout.Label("NEIGHBOUR ROOMS", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Neighbours allow AI to pass between them. \n IMPORTANT: Lock Room first to add Neighbours", MessageType.Info);
        string listString = "";

        if (selectedRoom.neighbourRooms.Count == 0)
        {
            listString += "No Neighbours in selected Room \n";
        }

        foreach (Room r in selectedRoom.neighbourRooms)
        {
            if (r == null)
            {
                listString += "Empty \n";
            }
            else
            {
                listString += r.name + "\n";
            }
        }
        GUILayout.TextArea(listString, EditorStyles.linkLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Rooms"))
        {
            List<Room> roomSelection = getObjectsFromSelection<Room>();

            foreach (Room r in roomSelection)
            {
                if (r == selectedRoom) continue;

                if (!selectedRoom.neighbourRooms.Contains(r)) {
                    selectedRoom.neighbourRooms.Add(r);

                    if (!r.neighbourRooms.Contains(selectedRoom)) {
                        r.neighbourRooms.Add(selectedRoom);                    
                    }
                }

            }

            cleanList<Room>(selectedRoom.neighbourRooms);
            EditorUtility.SetDirty(selectedRoom);

        }

        if (GUILayout.Button("Remove Rooms"))
        {
            List<Room> roomSelection = getObjectsFromSelection<Room>();

            foreach (Room r in roomSelection)
            {
                selectedRoom.neighbourRooms.Remove(r);
            }

            cleanList<Room>(selectedRoom.neighbourRooms);
            EditorUtility.SetDirty(selectedRoom);
        }

        GUILayout.EndHorizontal();
        /*
        if (GUILayout.Button("Select Neighbour Rooms"))
        {

            Selection.objects = selectedRoom.neighbourRooms.ToArray();
        }
        */
        EditorGUILayout.Space();

        string[] popUpOptions = new string[selectedRoom.neighbourRooms.Count];

        for (int i = 0; i < popUpOptions.Length; i++)
        {
            if (selectedRoom.neighbourRooms[i] != null)
            {
                popUpOptions[i] = selectedRoom.neighbourRooms[i].name;
            }
            else
            {
                popUpOptions[i] = "Empty";
            }

        }

        GUILayout.Label("Remove by selection", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        roomIndex = EditorGUILayout.Popup(roomIndex, popUpOptions);

        if (GUILayout.Button("Remove"))
        {
            if (roomIndex < selectedRoom.neighbourRooms.Count)
                selectedRoom.neighbourRooms.RemoveAt(roomIndex);

            EditorUtility.SetDirty(selectedRoom);
        }
        GUILayout.EndHorizontal();


    }



    bool createRoom()
    {
        if (Selection.activeObject == null)
        {
            GUILayout.Label("Selected Object: NONE", EditorStyles.boldLabel);
        }
        else
        {
            GUILayout.Label("Selected Object: " + Selection.activeObject.name, EditorStyles.boldLabel);

        }

        if (GUILayout.Button("Create Room Object"))
        {

            GameObject go = new GameObject();

            if (Selection.activeGameObject != null)
            {
                go.name = Selection.activeObject.name + " Room";

            }
            else
            {
                go.name = "New Room";
            }
            go.tag = "Room";

            if (roomGroup != null)
            {
                go.transform.parent = roomGroup.transform;
            }

            go.AddComponent<BoxCollider>().size = new Vector3(5, 5, 5);
            go.GetComponent<BoxCollider>().isTrigger = true;

            go.AddComponent<Room>().initialize();

            if (Selection.gameObjects.Length == 0)
            {
                go.transform.position = new Vector3(0, 0, 0);

            }
            else
            {
                Vector3 auxVector = new Vector3(0, 0, 0);
                foreach (GameObject gAux in Selection.gameObjects)
                {
                    auxVector += gAux.transform.position;
                }

                auxVector /= Selection.gameObjects.Length;

                go.transform.position = auxVector;
            }

            Selection.activeObject = go;

            EditorUtility.SetDirty(Selection.activeGameObject);
            return true;
        }


        //Add Room Component
        if (GUILayout.Button("Turn to Room"))
        {
            if (Selection.activeGameObject != null)
            {
                foreach (GameObject go in Selection.gameObjects)
                {
                    go.tag = "Room";

                    if (roomGroup != null)
                    {
                        go.transform.parent = roomGroup.transform;
                    }

                    if (go.GetComponent<Room>() == null)
                        go.AddComponent<Room>().initialize();

                    if (go.GetComponent<BoxCollider>() == null)
                    {
                        go.AddComponent<BoxCollider>().size = new Vector3(5, 5, 5);
                        go.AddComponent<BoxCollider>().isTrigger = true;

                    }

                    EditorUtility.SetDirty(Selection.activeGameObject);

                }


                return true;
            }

        }



        return false;
    }
    private void roomColliderMenu()
    {
        GUILayout.Label("SUB-COLLIDERS", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("Sub-Colliders make possible difficult shapes for rooms' collisions. \n Sub-Colliders are child objects of rooms", MessageType.Info);
        string listString = "";

        if (selectedRoom.GetComponentsInChildren<RoomCollider>().Length == 0)
        {
            listString = "No Sub-Colliders in selected Room";
        }

        foreach (RoomCollider rc in selectedRoom.GetComponentsInChildren<RoomCollider>())
        {

            if (rc == null)
            {
                listString += "Empty \n";
            }
            else
            {
                listString += rc.name + "\n";
            }
        }
        GUILayout.TextArea(listString, EditorStyles.linkLabel);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add SubCollider"))
        {
            GameObject go = new GameObject();
            go.name = "Sub-Collider";
            go.tag = "RoomCollider";
            go.transform.position = selectedRoom.transform.position;
            go.transform.parent = selectedRoom.transform;

            go.AddComponent<RoomCollider>();

            BoxCollider collider = go.AddComponent<BoxCollider>();

            collider.isTrigger = true;

            collider.size = new Vector3(5, 5, 5);


            Selection.activeObject = go;

        }
        GUILayout.EndHorizontal();


    }

    T getObjectFromSelection<T>()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            T myObject = go.GetComponent<T>();
            if (myObject != null)
            {
                return myObject;
            }
        }

        return default(T);
    }

    List<T> getObjectsFromSelection<T>()
    {
        List<T> myObjects = new List<T>();

        foreach (GameObject go in Selection.gameObjects)
        {
            T myComponent = go.GetComponent<T>();

            if (myComponent != null)
            {
                myObjects.Add(myComponent);
            }
        }

        return myObjects;
    }

    void cleanList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
            }
        }
    }

}

