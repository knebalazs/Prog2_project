using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_item
{
    public enum Item_type
    {
        food,
        fish,
        bottle,
        fishing_rod,
        lamp,
        bucket
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
            case Item_type.fish:
                return icon_assets.fish;
            case Item_type.bucket:
                return icon_assets.bucket;
            case Item_type.bottle:
                switch (GameObject.Find("player").GetComponent<stat_controller>().bottle_contains)
                {
                    case 2:
                        return icon_assets.bottle_full;
                    case 1:
                        return icon_assets.bottle_half;
                    case 0:
                        return icon_assets.bottle_empty;
                }
                return icon_assets.bottle_full;
        }
    }
}
