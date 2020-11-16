using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//PlayerComponent inheritance makes input interaction possible!
public class ScrollInventory : PlayerComponent
{
    public Item selectedItem { get; private set; }
    public RawImage image;
    public TextMeshProUGUI text;
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        SetItem(currentIndex);
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

    public void ChangeItem(bool direction)
    {
        if(direction)
        {
            if(Item.sharedInstance.itemIcons.Count - 1 == currentIndex)
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
                currentIndex = Item.sharedInstance.itemIcons.Count - 1;
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
        image.texture = Item.sharedInstance.itemIcons[index];
        text.text = Item.sharedInstance.itemNames[index];
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
