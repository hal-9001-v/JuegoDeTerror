using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//PlayerComponent inheritance makes input interaction possible!
public class ScrollInventory : PlayerComponent
{
    public static ScrollInventory sharedInstance;

    public Item selectedItem { get; private set; }
    public List<Item> currentItems;
    public RawImage image;
    public TextMeshProUGUI text;
    public int currentIndex;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentItems = new List<Item>();
        
        currentIndex = currentItems.Count;
        image.CrossFadeAlpha(0.0f, 0.0f, false);
    }

    void upPressed()
    {
        //Do some stuff when up is pressed
        Debug.Log("Up");

        ChangeItem(true);
    }

    void downPressed()
    {
        Debug.Log("Down");
        //Do some stuff when down is pressed

        ChangeItem(false);
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

    public void DeleteItem(Item newItem)
    {
        currentItems.Remove(newItem);

        ChangeItem(true);
    }

    public IEnumerator NameFadeOut()
    {
        int count = 0;

        if (count == 0)
        {
            yield return new WaitForSeconds(3.0f);
            count++;
        }

        text.CrossFadeAlpha(0f, 2.0f, false);
        yield return null;
    }

    public void ChangeItem(bool direction)
    {
        if(direction)
        {
            if(currentItems.Count - 1 == currentIndex)
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
            if (currentIndex == 0)
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
        image.texture = currentItems[index].itemIcon;
        text.text = currentItems[index].itemName;

        StartCoroutine(NameFadeOut());
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
    }
}
