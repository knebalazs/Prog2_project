using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class my_equip : my_inventory {

    List<my_item> equip_slots = new List<my_item>() { null, null};
    player_movement player_ref = GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>();



    public void equipping(int equip_bar_pos, int inventory_pos)
    {
        equip_slots[equip_bar_pos] = player_ref.invent.get_item(inventory_pos);
        player_ref.invent.remove_item(inventory_pos);
        player_ref.ui_inventory.refresh_inventory();
        refresh_equip();

        switch (equip_slots[equip_bar_pos].item_type) {
            default:
            case my_item.Item_type.fishing_rod:
                GameObject.FindGameObjectWithTag("fishing").GetComponent<fishing>().is_equipped = true;
                break;
            case my_item.Item_type.lamp:
                player_ref.lamp.SetActive(true);
                break;
            case my_item.Item_type.food:
                break;
            }
    }

    public void unequipping(int equip_bar_pos, int inventory_pos)
    {
        player_ref.invent.item_list[inventory_pos] = equip_slots[equip_bar_pos];
        equip_slots[equip_bar_pos] = null;
        refresh_equip();
        player_ref.ui_inventory.refresh_inventory();

        switch (player_ref.invent.item_list[inventory_pos].item_type)
        {
            default:
            case my_item.Item_type.fishing_rod:
                GameObject.FindGameObjectWithTag("fishing").GetComponent<fishing>().is_equipped = false;
                break;
            case my_item.Item_type.lamp:
                player_ref.lamp.SetActive(false);
                break;
            case my_item.Item_type.food:
                break;
        }
    }

    public void refresh_equip()
    {
        int counter = 0;
        foreach (my_item item in equip_slots)
        {
            if (item != null)
            {
                GameObject.Find("equip").transform.GetChild(counter).GetChild(0).gameObject.SetActive(true);
                Image image = GameObject.Find("equip").transform.GetChild(counter).GetChild(0).GetComponent<Image>();
                image.sprite = item.set_sprite();
            }
            else
                GameObject.Find("equip").transform.GetChild(counter).GetChild(0).gameObject.SetActive(false);

            counter++;
        }
    }
}
