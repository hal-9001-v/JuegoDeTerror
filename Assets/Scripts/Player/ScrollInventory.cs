using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerComponent inheritance makes input interaction possible!
public class ScrollInventory : PlayerComponent
{
    public Item selectedItem { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    void upPressed()
    {
        //Do some stuff when up is pressed
        Debug.Log("Up");
    }

    void downPressed()
    {
        Debug.Log("Down");
        //Do some stuff when down is pressed
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
