using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    private string safePath;
    private string tempPath;
    private string statsPath;

    public float defaultGamepadSens = 0.25f;
    public float defaultMouseSens = 3;
    public float defaultVolume = 5;

    private void Awake()
    {
        Debug.Log(gameObject.name);

        if (instance == null)
        {
            instance = this;
            tempPath = Application.persistentDataPath + "/tempSave.dat";
            safePath = Application.persistentDataPath + "/safeSave.dat";
            statsPath = Application.persistentDataPath + "/statsSave.dat";

        }
        else
        {
            Debug.LogWarning(gameObject.name + " has been deleted, to keep " + instance.name + ". There must be only one singleton!");

            Destroy(this);
        }
    }

    public void deleteData()
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
    public void saveGame()
    {
        Debug.Log("SAVE");

        //Replace safeFile with tempFile
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

        //Create new tempFile
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            //Make new Temp file, which is unsafe for now
            FileStream file = File.Open(tempPath, FileMode.Create);

            GameData data = new GameData();

            savePlayer(data);
            saveInventory(data);
            savePursuer(data);

            saveInteractables(data);
            saveDialogues(data);

            saveTask(data);

            formatter.Serialize(file, data);

            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void saveStats(StatsData data)
    {
        if (data != null)
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
    }

    public StatsData loadStats()
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

    private void savePlayer(GameData data)
    {
        Vector3 pos = PlayerMovement.sharedInstance.transform.position;
        Vector3 rotation = PlayerMovement.sharedInstance.transform.eulerAngles;

        Vector3 cameraRotation = CameraLook.sharedInstance.transform.eulerAngles;

        data.myPlayerData = new PlayerData(pos, rotation, cameraRotation);
    }

    private void saveInventory(GameData data)
    {
        /*
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
        }*/

    }

    private void savePursuer(GameData data)
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

    private void saveDialogues(GameData data)
    {
        DialogueTrigger[] dialogueTriggers = FindObjectsOfType<DialogueTrigger>();

        DialogueData[] saveDatas = new DialogueData[dialogueTriggers.Length];

        for (int i = 0; i < dialogueTriggers.Length; i++)
        {
            saveDatas[i] = dialogueTriggers[i].getSaveData();
        }

        data.myDialoguesData = saveDatas;
    }

    private void saveTask(GameData data)
    {
        TaskController.instance.saveData(data);
    }

    public void loadGame()
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

                if (data != null)
                {
                    loadPlayer(data);
                    loadInventory(data);
                    loadIA(data);

                    loadInteractables(data);
                    loadDialogues(data);

                    loadTask(data);

                }
                else
                {
                    Debug.LogError("No Save Data!");
                }


            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void loadPlayer(GameData data)
    {
        Vector3 position, rotation, cameraRotation;

        position = data.myPlayerData.playerPosition.GetVector3();
        rotation = data.myPlayerData.playerRotation.GetVector3();
        cameraRotation = data.myPlayerData.cameraRotation.GetVector3();

        PlayerMovement.sharedInstance.transform.position = position;
        PlayerMovement.sharedInstance.transform.eulerAngles = rotation;

        CameraLook.sharedInstance.transform.eulerAngles = cameraRotation;

    }

    private void loadInventory(GameData data)
    {
        /*
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
        */
    }

    private void loadIA(GameData data)
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
                Debug.LogError("Scene has change since previous save!");
            }

        }


    }

    private void loadDialogues(GameData data)
    {
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();

        if (triggers.Length != data.myDialoguesData.Length)
        {
            Debug.LogError("Scene has changed! cant load interactables from save data");
        }

        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].name == data.myDialoguesData[i].dialogueName)
            {
                triggers[i].loadData(data.myDialoguesData[i]);
            }
            else
            {
                Debug.LogError("Scene has change since last save!");
            }
        }
    }

    private void loadTask(GameData data)
    {
        TaskController.instance.loadData(data);
    }
}
