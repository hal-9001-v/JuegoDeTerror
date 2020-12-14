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
        }
    }

    void Start()
    {
        currentItems = new List<Item>();

        currentIndex = 0;
        image.CrossFadeAlpha(0.0f, 0.0f, false);
        SetItem(currentIndex);

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
            SetItem(0);
        }
    }

    public void DeleteItem(Item item)
    {

        if (currentItems.Remove(item))
        {
            currentIndex = 0;
            SetItem(currentIndex);
        }


    }

    //Coroutine that makes disappear item's name after x time
    public IEnumerator NameFadeOut()
    {
        int count = 0;

        if (count == 0)
        {
            yield return new WaitForSeconds(timeUntilNameFadeHappens);
            count++;
        }

        text.CrossFadeAlpha(0f, nameFadeTime, false);
        yield return null;
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

        SetItem(currentIndex);
    }

    public void SetItem(int index)
    {
        text.CrossFadeAlpha(1.0f, 0.0f, false);
        if (currentItems.Count != 0)
        {

            image.color = new Vector4(1, 1, 1, 1);
            image.texture = currentItems[index].itemIcon;
            text.text = currentItems[index].itemName;
        }
        else
        {
            image.texture = null;
            image.color = new Vector4(0, 0, 0, 0);
            text.text = "Empty";
        }

        StartCoroutine(NameFadeOut());

        if (index < currentItems.Count && index >= 0)
            selectedItem = currentItems[index];
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
