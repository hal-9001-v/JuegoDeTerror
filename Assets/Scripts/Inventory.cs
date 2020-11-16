using UnityEngine;
using UnityEngine.UI;
/*
public class Inventory : MonoBehaviour
{
    public const int numItems = 5;
    public Item[] inventoryItems = new Item[numItems];
    public GameObject[] inventorySquares = new GameObject[numItems];
    public Item[] allItems = new Item[2];

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

        inventoryItems[counter] = null;
        Destroy(inventorySquares[counter].transform.GetChild(0).gameObject);
    }

    public void DeleteAllItems()
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i] = null;
            try
            {
                Destroy(inventorySquares[i].transform.GetChild(0).gameObject);
            }
            catch (System.Exception e)
            {
                Debug.Log("Inventory. La casilla " + i + " está vacía." + e);
            }
        }
    }

    public void LoadInventory(string[] loadItemNames)
    {
        DeleteAllItems();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            for(int j = 0; j < allItems.Length; j++)
            {
                if(loadItemNames[i] == allItems[j].itemName)
                {
                    AddItem(allItems[j]);
                }
            }
        }
    }
}
*/