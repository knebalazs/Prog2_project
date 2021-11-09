using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class my_inventory
{
    public List<my_item> item_list;
    public int slot_size = 16;

    public my_inventory()
    {
        item_list = new List<my_item>();
        for (int i = 0; i < slot_size; i++) item_list.Add(null);

        add_item(new my_item { item_type = my_item.Item_type.lamp, amount = 1});
        add_item(new my_item { item_type = my_item.Item_type.fishing_rod, amount = 1 });
        add_item(new my_item { item_type = my_item.Item_type.fishing_rod, amount = 1 });
        add_item(new my_item { item_type = my_item.Item_type.fishing_rod, amount = 1 });
    }

    public my_item get_item(int pos)
    {
        return item_list[pos];
    }

    public void add_item(my_item item)
    {
        for (int i = 0; i < slot_size; i++)
            if (item_list[i] == null)
            {
                item_list[i] = item;
                break;
            }
    }

    public void remove_item(int pos)
    {
        item_list[pos] = null;
    }

    public void move_item(int pos_a, int pos_b)
    {
        if (item_list[pos_b] == null)
        {
            item_list[pos_b] = item_list[pos_a];
            item_list[pos_a] = null;
        }
        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().ui_inventory.refresh_inventory();
    }

    public List<my_item> get_item_list()
    {
        return item_list;
    }
}
