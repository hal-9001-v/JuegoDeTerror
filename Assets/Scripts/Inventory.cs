using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int numItems = 5;
    public Item[] inventoryItems = new Item[numItems];
    public GameObject[] inventorySquares = new GameObject[numItems];

    public static Inventory sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void AddItem(Item itemToAdd)
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int counter = -1;
            bool found = false;
            do
            {
                counter++;
                if (inventoryItems[counter] == null)
                {
                    found = true;
                }
            } while (found == false && counter < inventoryItems.Length);

            inventoryItems[counter] = itemToAdd;
            Texture image = itemToAdd.icon;
            GameObject g = new GameObject();
            g.AddComponent<RawImage>();
            g.GetComponent<RawImage>().texture = image;

            g.transform.SetParent(inventorySquares[counter].transform);
            g.transform.localPosition = Vector3.zero;

        }
    }

    public void DeleteItem(Item itemToDelete)
    {
        bool found = false;
        int counter = -1;

        do
        {
            counter++;
            if (itemToDelete.itemName == inventoryItems[counter].itemName)
            {
                found = true;
            }
        } while (found == false && counter < inventoryItems.Length);
        /*
        for(int i = counter; i < (itemsCounter - 1); i++)
        {
            inventoryItems[i] = inventoryItems[i + 1];
            Destroy(inventorySquares[i].transform.GetChild(0).gameObject);
        }

        inventoryItems[itemsCounter - 1] = null;
        Destroy(inventorySquares[itemsCounter - 1].transform.GetChild(0).gameObject);
        itemsCounter--;
        */

        inventoryItems[counter] = null;
        Destroy(inventorySquares[counter].transform.GetChild(0).gameObject);
    }

    public void AddItemInPosition()
    {

    }
}
