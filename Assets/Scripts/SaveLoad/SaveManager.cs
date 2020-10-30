using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor.Purchasing;
using UnityEngine.UI;

public static class SaveManager 
{
    private static string safePath = Application.persistentDataPath + "/safeSave.dat";
    private static string tempPath = Application.persistentDataPath + "/tempSave.dat";
    private static string statsPath = Application.persistentDataPath + "/statsSave.dat";

    public static float defaultGamepadSens = 0.25f;
    public static float defaultMouseSens = 3;
    public static float defaultVolume = 5;

    // Start is called before the first frame update
   

    public static void deleteData()
    {
        try
        {
            File.Delete(safePath);
            File.Delete(tempPath);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }

    }
    public static void SaveGame()
    {
        Debug.Log("SAVE");

        try
        {
            if (File.Exists(safePath))
            {
                File.Delete(safePath);
            }

            //Temp file is now safe
            File.Move(tempPath, safePath);

        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        };

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            //Make new Temp file, which is unsafe for now
            FileStream file = File.Open(tempPath, FileMode.Create);

            GameData data = new GameData();

            SavePlayer(data);
            SaveInventory(data);
            SavePursuer(data);
            saveInteractables(data);
            saveTask(data);

            formatter.Serialize(file, data);

            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public static void saveStats(StatsData data)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = File.Open(statsPath, FileMode.Create);

            formatter.Serialize(file, data);

            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public static StatsData loadStats()
    {

        try
        {
            if (File.Exists(statsPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                FileStream file = File.Open(statsPath, FileMode.Open);

                StatsData data = (StatsData)formatter.Deserialize(file);

                file.Close();

                return data;

            }
            else
            {
                StatsData newStats = new StatsData();
                newStats.gamePadSens = defaultGamepadSens;
                newStats.mouseSens = defaultMouseSens;
                newStats.volume = defaultVolume;

                saveStats(newStats);

                return newStats;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);

        }
        return null;
    }

    private static void SavePlayer(GameData data)
    {
        Vector3 pos = PlayerMovement.sharedInstance.transform.position;
        Vector3 rotation = PlayerMovement.sharedInstance.transform.eulerAngles;

        Vector3 cameraRotation = CameraLook.sharedInstance.transform.eulerAngles;

        data.myPlayerData = new PlayerData(pos, rotation, cameraRotation);
    }

    private static void SaveInventory(GameData data)
    {
        if (Inventory.sharedInstance != null)
        {

            string[] inventory = new string[5];

            for (int i = 0; i < inventory.Length; i++)
            {
                if (Inventory.sharedInstance.inventoryItems[i] != null)
                {

                    inventory[i] = Inventory.sharedInstance.inventoryItems[i].itemName;
                }
                else
                {
                    inventory[i] = "empty";
                }
            }

            data.myInventory = new PlayerInventory(inventory);
        }
        else
        {
            Debug.LogWarning("There is no Inventory in Scene");
        }

    }

    private static void SavePursuer(GameData data)
    {
        if (Pursuer.instance != null)
        {
            data.myPursuerData = Pursuer.instance.getSaveData();

        }
        else
        {
            Debug.LogWarning("There is no Pursuer in Scene");
        }

    }

    private static void saveInteractables(GameData data)
    {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();

        InteractableData[] saveDatas = new InteractableData[interactables.Length];

        for (int i = 0; i < interactables.Length; i++)
        {
            saveDatas[i] = interactables[i].getSaveData();
        }

        data.myInteractablesData = saveDatas;

    }

    private static void saveTask(GameData data)
    {
        TaskController.instance.saveData(data);
    }

    public static void LoadGame()
    {
        Debug.Log("Load");
        try
        {
            if (File.Exists(safePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                FileStream file = File.Open(safePath, FileMode.Open);

                GameData data = (GameData)formatter.Deserialize(file);

                file.Close();

                LoadPlayer(data);
                LoadInventory(data);
                LoadIA(data);
                loadInteractables(data);
                loadTask(data);

            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }


    private static void LoadPlayer(GameData data)
    {
        Vector3 position, rotation, cameraRotation;

        position = data.myPlayerData.playerPosition.GetVector3();
        rotation = data.myPlayerData.playerRotation.GetVector3();
        cameraRotation = data.myPlayerData.cameraRotation.GetVector3();

        PlayerMovement.sharedInstance.transform.position = position;
        PlayerMovement.sharedInstance.transform.eulerAngles = rotation;

        CameraLook.sharedInstance.transform.eulerAngles = cameraRotation;

    }

    private static void LoadInventory(GameData data)
    {
        if (Inventory.sharedInstance != null)
        {
            string[] myInventory = new string[5];

            for (int i = 0; i < myInventory.Length; i++)
            {
                myInventory[i] = data.myInventory.objectNames[i];
                Debug.Log(myInventory[i]);
            }

            Inventory.sharedInstance.LoadInventory(myInventory);
        }
        else
        {
            //Debug.LogWarning("There is no Inventory in Scene");
        }

    }

    private static void LoadIA(GameData data)
    {
        if (Pursuer.instance != null)
        {
            Pursuer.instance.loadData(data.myPursuerData);
        }
        else
        {
            Debug.LogWarning("There is no Pursuer in Scene");
        }

    }

    private static void loadInteractables(GameData data)
    {
        Interactable[] interactables = GameObject.FindObjectsOfType<Interactable>();

        if (interactables.Length != data.myInteractablesData.Length)
        {
            Debug.LogError("Scene has changed! Can't load Interactables from save data");
            return;

        }

        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactables[i].gameObject.name == data.myInteractablesData[i].interactableName)
            {
                interactables[i].loadData(data.myInteractablesData[i]);
            }
            else
            {
                Debug.LogError("Scene has changed! Can't load Interactables from save data");
            }

        }


    }

    private static void loadTask(GameData data)
    {
        TaskController.instance.loadData(data);
    }
}
