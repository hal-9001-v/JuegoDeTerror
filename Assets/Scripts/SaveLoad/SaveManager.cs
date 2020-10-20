using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor.Purchasing;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        path = Application.persistentDataPath + "/save.dat";
    }


    public void Save()
    {
        Debug.Log("SAVE");
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.OpenOrCreate);

            GameData data = new GameData();

            SavePlayer(data);
            SaveInventory(data);
            SavePursuer(data);
            saveInteractables(data);

            formatter.Serialize(file, data);

            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void SavePlayer(GameData data)
    {
        Vector3 pos = PlayerMovement.sharedInstance.transform.position;
        Vector3 rotation = PlayerMovement.sharedInstance.transform.eulerAngles;

        Vector3 cameraRotation = CameraLook.sharedInstance.transform.eulerAngles;

        data.myPlayerData = new PlayerData(pos, rotation, cameraRotation);
    }

    private void SaveInventory(GameData data)
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

    private void SavePursuer(GameData data)
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

    private void saveInteractables(GameData data)
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();

        InteractableData[] saveDatas = new InteractableData[interactables.Length];

        for (int i = 0; i < interactables.Length; i++)
        {
            saveDatas[i] = interactables[i].getSaveData();
        }

        data.myInteractablesData = saveDatas;

    }


    public void Load()
    {
        try
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                FileStream file = File.Open(path, FileMode.Open);

                GameData data = (GameData)formatter.Deserialize(file);

                file.Close();

                LoadPlayer(data);
                LoadInventory(data);
                LoadIA(data);
                loadInteractables(data);

            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void LoadPlayer(GameData data)
    {
        Vector3 position, rotation, cameraRotation;

        position = data.myPlayerData.playerPosition.GetVector3();
        rotation = data.myPlayerData.playerRotation.GetVector3();
        cameraRotation = data.myPlayerData.cameraRotation.GetVector3();

        PlayerMovement.sharedInstance.transform.position = position;
        PlayerMovement.sharedInstance.transform.eulerAngles = rotation;

        CameraLook.sharedInstance.transform.eulerAngles = cameraRotation;

    }

    private void LoadInventory(GameData data)
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

    private void LoadIA(GameData data)
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

    private void loadInteractables(GameData data)
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();

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

}
