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


    [MenuItem("Tools/Room Tool")]
    public static void showWindow()
    {
        GetWindow(typeof(RoomTool));
    }

    private void OnGUI()
    {

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

        lightMenu();

        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.Space();
        }

        roomMenu();

        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.Space();
        }

        EditorGUILayout.HelpBox("Removed Objects aren't eliminated from scene", MessageType.Warning);

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
        if (GUILayout.Button("Add N. Rooms"))
        {
            List<Room> roomSelection = getObjectsFromSelection<Room>();

            foreach (Room r in roomSelection)
            {
                selectedRoom.neighbourRooms.Add(r);
            }

            cleanList<Room>(selectedRoom.neighbourRooms);
            EditorUtility.SetDirty(selectedRoom);

        }

        if (GUILayout.Button("Remove N. Rooms"))
        {
            List<Room> roomSelection = getObjectsFromSelection<Room>();

            foreach (Room r in roomSelection)
            {
                selectedRoom.neighbourRooms.Remove(r);
            }

            cleanList<Room>(selectedRoom.neighbourRooms);
            EditorUtility.SetDirty(selectedRoom);
        }

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

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

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

