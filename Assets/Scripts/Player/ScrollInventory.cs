using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//PlayerComponent inheritance makes input interaction possible!
public class ScrollInventory : PlayerComponent
{
    public static ScrollInventory sharedInstance;

    [Tooltip("Image of the inventory square")]
    public RawImage image;

    [Tooltip("Item's text name behind the inventory square")]
    public TextMeshProUGUI text;

    [Header("Text's Fade")]

    [Tooltip("Time before text start fading")]
    public float timeUntilNameFadeHappens = 3.0f;
    [Range(0.01f, 1)]
    public float typeDelay = 0.01f;

    [Tooltip("Fade time")]
    public float nameFadeTime = 2.0f;

    private List<Item> currentItems;
    private int currentIndex;

    public Item selectedItem { get; private set; }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;

            currentItems = new List<Item>();
        }
    }

    void Start()
    {
        currentIndex = 0;
        image.CrossFadeAlpha(0.0f, 0.0f, false);
        setItem(currentIndex);

        if (GameEventManager.sharedInstance != null)
        {
            //Subscribing to GameEventManager events...
            GameEventManager.sharedInstance.OnAddedItemToInventory += GameEventManager_OnAddedItemToInventory;
            GameEventManager.sharedInstance.OnDeletedItemToInventory += GameEventManager_OnDeletedItemToInventory;
        }

    }

    //When this object is destroyed we unsubscribed to avoid future null pointer errors
    public void OnDestroy()
    {
        if (GameEventManager.sharedInstance != null)
        {
            GameEventManager.sharedInstance.OnAddedItemToInventory -= GameEventManager_OnAddedItemToInventory;
            GameEventManager.sharedInstance.OnDeletedItemToInventory -= GameEventManager_OnDeletedItemToInventory;
        }

    }

    private void GameEventManager_OnDeletedItemToInventory(object sender, GameEventManager.OnUsedItemForInventory e)
    {
        if (currentItems.Count != 0)
        {
            DeleteItem(e.myItem);
        }
    }

    private void GameEventManager_OnAddedItemToInventory(object sender, GameEventManager.OnUsedItemForInventory e)
    {
        AddItem(e.myItem);
    }

    void upPressed()
    {
        //Do some stuff when up is pressed

        ChangeItem(true);
    }

    void downPressed()
    {
        //Do some stuff when down is pressed
        ChangeItem(false);
    }

    void useItem()
    {
        if (selectedItem != null)
        {
            selectedItem.useItem();
        }

    }

    public void AddItem(Item newItem)
    {
        currentItems.Add(newItem);

        if (currentItems.Count == 1)
        {
            image.CrossFadeAlpha(1.0f, 0.0f, false);
            setItem(0);
        }
    }

    public void DeleteItem(Item item)
    {

        if (currentItems.Remove(item))
        {
            currentIndex = 0;
            setItem(currentIndex);
        }


    }

    public void ChangeItem(bool direction)
    {
        if (direction)
        {
            if (currentItems.Count - 1 <= currentIndex)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
        }
        else
        {
            if (currentIndex <= 0)
            {
                currentIndex = currentItems.Count - 1;
            }
            else
            {
                currentIndex--;
            }
        }

        setItem(currentIndex);
    }

    public void setItem(int index)
    {
        text.CrossFadeAlpha(1.0f, 0.0f, false);
        if (currentItems.Count != 0 && index < currentItems.Count && index >= 0)
        {

            image.color = new Vector4(1, 1, 1, 1);
            image.texture = currentItems[index].itemIcon;

            selectedItem = currentItems[index];
            StopAllCoroutines();
            StartCoroutine(typeCurrentItem(selectedItem.itemName));

        }
        else
        {
            image.texture = null;
            image.color = new Vector4(0, 0, 0, 0);
            text.text = "Empty";
        }

    }

    IEnumerator typeCurrentItem(string itemName)
    {

        text.text = "";

        foreach (char c in itemName.ToCharArray())
        {
            text.text += c;

            yield return new WaitForSeconds(typeDelay);

        }

        yield return new WaitForSeconds(timeUntilNameFadeHappens);

        text.CrossFadeAlpha(0f, nameFadeTime, false);

    }


    public void loadData(InventoryData data)
    {
        GameObject go;
        Item goItem;
        currentItems.Clear();

        if (data.itemNames != null)
        {
            for (int i = 0; i < data.itemNames.Length; i++)
            {
                go = GameObject.Find(data.itemNames[i]);

                if (go != null)
                {
                    goItem = go.GetComponent<Item>();

                    if (goItem != null)
                    {
                        currentItems.Add(goItem);
                    }
                    else
                    {
                        Debug.LogError("Gameobject " + go.name + " saved as Item but has no Item Component!");
                    }
                }
                else
                {
                    Debug.LogError("Can't find " + data.itemNames[i] + " Item in scene!");

                }

            }
        }
        setItem(0);

    }

    public InventoryData getSaveData()
    {

        Item[] items = currentItems.ToArray();
        InventoryData data = new InventoryData();

        data.itemNames = new string[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            data.itemNames[i] = items[i].gameObject.name;
        }

        return data;
    }

    public override void setPlayerControls(PlayerControls pc)
    {
        pc.Normal.InventoryAxis.performed += ctx =>
        {
            float verticalInput = ctx.ReadValue<float>();

            if (verticalInput > 0)
                upPressed();
            else if (verticalInput < 0)
                downPressed();

        };

        pc.Normal.UseItem.performed += ctx =>
        {
            useItem();
        };
    }
}
