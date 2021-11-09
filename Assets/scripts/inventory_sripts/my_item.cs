using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_item
{
    public enum Item_type
    {
        food,
        drink,
        fishing_rod,
        lamp,
    }

    public Item_type item_type;
    public int amount;
    private item_sprites icon_assets = GameObject.FindGameObjectWithTag("icons").GetComponent<item_sprites>();

    public Sprite set_sprite()
    {
        switch (item_type)
        {
            default:
            case Item_type.lamp:
                return icon_assets.lamp_sprite;
            case Item_type.fishing_rod:
                return icon_assets.fishing_rod_sprite;
        }
    }
}
