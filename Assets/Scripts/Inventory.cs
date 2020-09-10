using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int numItems = 5;
    public Item[] inventoryItems = new Item[numItems];
    public GameObject[] inventorySquares = new GameObject[numItems];

    private int itemsCounter = 0;
    private int activeSquaresCounter = 0;

    public void AddItem(Item itemToAdd)
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            inventoryItems[itemsCounter] = itemToAdd;
            itemsCounter++;
            Texture image = itemToAdd.icon;
            GameObject g = new GameObject();
            g.AddComponent<RawImage>();
            g.GetComponent<RawImage>().texture = image;

            g.transform.SetParent(inventorySquares[itemsCounter].transform);
            g.transform.localPosition = Vector3.zero;
            
        }
    }

    public void DeleteItem()
    {

    }
}
