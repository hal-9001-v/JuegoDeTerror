﻿using System.Collections;
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
            SaveInventory(data);

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

    private void SaveInventory(GameData data)
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

    private void SaveIA(GameData data)
    {

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
                LoadInventory(data);
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
        position = data.myPlayerData.playerPosition.GetVector3();

        Debug.Log(position.x + " " + position.y + " " + position.z);
        PlayerMovement.sharedInstance.transform.position = position;
    }

    private void LoadInventory(GameData data)
    {
        string[] myInventory = new string[5];

        for (int i = 0; i < myInventory.Length; i++)
        {
            myInventory[i] = data.myInventory.objectNames[i];
            Debug.Log(myInventory[i]);
        }

        Inventory.sharedInstance.LoadInventory(myInventory);
    }

    private void LoadIA(GameData data)
    {

    }
}
