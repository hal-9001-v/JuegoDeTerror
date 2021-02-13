using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    private string safePath;
    //private string tempPath;
    private string statsPath;

    public float defaultGamepadSens = 0.25f;
    public float defaultMouseSens = 3;
    public float defaultVolume = 5;

    public Torch torch;

    // GameData safeData;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            safePath = Application.persistentDataPath + "/safeSave.dat";
            statsPath = Application.persistentDataPath + "/statsSave.dat";

        }
        else
        {
            instance.torch = torch;

            if (instance.torch == null) {
                instance.torch = FindObjectOfType<Torch>();
            }

            Debug.LogWarning(gameObject.name + " has been deleted, to keep " + instance.name + ". There must be only one singleton!");

            Destroy(this);
        }
    }

    public void deleteData()
    {
        try
        {
            File.Delete(safePath);
            //File.Delete(tempPath);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }

    }
    public void saveGame()
    {
        Debug.Log("Save Game");

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            //Make new Temp file, which is unsafe for now
            FileStream file = File.Open(safePath, FileMode.Create);

            formatter.Serialize(file, getCurrentGameData());

            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    GameData getCurrentGameData()
    {
        GameData data = new GameData();

        savePlayer(data);
        saveInventory(data);
        savePursuer(data);

        saveInteractables(data);
        saveTriggers(data);

        saveRadioZones(data);

        saveTask(data);

        return data;
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

        bool ready = false;
        bool lit = false;

        if (torch != null)
        {
            ready = torch.readyToUse;
            lit = torch.myLight.enabled;

        }

        data.myPlayerData = new PlayerData(pos, rotation, cameraRotation, ready, lit);
    }

    private void saveInventory(GameData data)
    {
        if (ScrollInventory.sharedInstance != null)
        {
            data.myInventory = ScrollInventory.sharedInstance.getSaveData();
        }
        else
        {
            Debug.LogError("There is no Inventory in Scene!");
        }

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

    private void saveTriggers(GameData data)
    {
        Trigger[] triggers = FindObjectsOfType<Trigger>();

        TriggerData[] saveDatas = new TriggerData[triggers.Length];

        for (int i = 0; i < triggers.Length; i++)
        {
            saveDatas[i] = triggers[i].getSaveData();
        }

        data.myTriggerData = saveDatas;
    }

    private void saveRadioZones(GameData data)
    {
        RadioZone[] zones = FindObjectsOfType<RadioZone>();

        RadioZoneData[] zoneDatas = new RadioZoneData[zones.Length];

        for (int i = 0; i < zones.Length; i++) {
            zoneDatas[i] = zones[i].getSaveData();

        }

        data.myRadioZoneData = zoneDatas;

    }

    private void saveTask(GameData data)
    {
        TaskController.instance.saveData(data);
    }

 

    public void loadGame()
    {
        Debug.Log("Load Game");
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
                    //safeData = data;

                    loadPlayer(data);
                    loadTask(data);

                    loadInventory(data);
                    loadIA(data);

                    loadInteractables(data);
                    loadTriggers(data);

                    loadRadioZones(data);

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

        CameraLook.sharedInstance.skipUpdate = true;
        CameraLook.sharedInstance.transform.eulerAngles = cameraRotation;

        if (torch != null)
        {
            torch.myLight.enabled = data.myPlayerData.torchIsLit;
            torch.readyToUse = data.myPlayerData.torchReadyToUse;
        }

    }

    private void loadInventory(GameData data)
    {

        if (ScrollInventory.sharedInstance != null)
        {
            if (data.myInventory != null)
                ScrollInventory.sharedInstance.loadData(data.myInventory);
            else
                Debug.LogError("NULL IN INVENTORY");
        }
        else
        {
            Debug.LogError("There is no Inventory in scene!");
        }

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
                Debug.LogError("Check " + interactables[i].gameObject.name + " " + data.myInteractablesData[i].interactableName);
            }

        }


    }

    private void loadTriggers(GameData data)
    {
        DialogueTrigger[] triggers = FindObjectsOfType<DialogueTrigger>();

        if (triggers.Length != data.myTriggerData.Length)
        {
            Debug.LogError("Scene has changed! can't load interactables from save data!");
            return;
        }

        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].name == data.myTriggerData[i].triggerName)
            {
                triggers[i].loadData(data.myTriggerData[i]);
            }
            else
            {
                Debug.LogError("Scene has change since last save!");
            }
        }
    }

    private void loadRadioZones(GameData data) {
        RadioZone[] zones = FindObjectsOfType<RadioZone>();

        if (zones.Length != data.myRadioZoneData.Length) {
            Debug.LogError("Scene has changed! can't load Radio Zones from save data!");
            return;
        }

        for (int i = 0; i < zones.Length; i++) {
            if (zones[i].name == data.myRadioZoneData[i].radioName) {
                zones[i].loadData(data.myRadioZoneData[i]);
            }
            else{
                Debug.LogError("Scene has change since last save!");
            }
        
        }

    }

    private void loadTask(GameData data)
    {
        TaskController.instance.loadData(data);
    }
}
