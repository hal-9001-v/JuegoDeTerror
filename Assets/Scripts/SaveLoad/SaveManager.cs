using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        path = Application.persistentDataPath + "/save.dat";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Load();
        }
    }

    private void Save()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.OpenOrCreate);

            GameData data = new GameData();

            SavePlayer(data);

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
        data.myPlayerData = new PlayerData(pos);
    }

    private void Load()
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
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void LoadPlayer(GameData data)
    {
        Vector3 position;
        position.x = data.myPlayerData.playerPosition[0];
        position.y = data.myPlayerData.playerPosition[1];
        position.z = data.myPlayerData.playerPosition[2];

        Debug.Log(position.x);
        PlayerMovement.sharedInstance.transform.position = position;
    }
}
