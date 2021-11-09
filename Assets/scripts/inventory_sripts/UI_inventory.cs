using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
    
    
    private my_inventory inventory;



    public void set_inventory(my_inventory inventory)
    {
        this.inventory = inventory;
        refresh_inventory();
    }
    
    public void refresh_inventory()
    {
        int counter = 0;
        foreach(my_item item in inventory.get_item_list())
        {
            if (item != null)
            {
                this.transform.GetChild(counter).GetChild(0).gameObject.SetActive(true);
                Image image = this.transform.GetChild(counter).GetChild(0).GetComponent<Image>();
                image.sprite = item.set_sprite();
            }
            else
                this.transform.GetChild(counter).GetChild(0).gameObject.SetActive(false);
            
            counter++;
        }
    }
}
