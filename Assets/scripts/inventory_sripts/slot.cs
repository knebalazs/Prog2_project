using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IDropHandler
{

    public int find_pos(GameObject obj)
    {
        int counter = 0;


        foreach (Transform child in obj.transform.parent)
            if (child.gameObject == obj.gameObject)
                return counter;
            else
                counter++;
        return -1;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().is_draging_item = false;
        int start_pos = find_pos(eventData.pointerDrag.transform.parent.gameObject);
        int end_pos = find_pos(this.gameObject);
        

        if (eventData.pointerDrag != null && !this.transform.GetChild(0).gameObject.activeSelf)
        {
            eventData.pointerDrag.transform.position = eventData.pointerDrag.GetComponent<drag_drop>().begin_pos;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
            
             switch (this.transform.parent.name) {
                 case "equip":
                    if (eventData.pointerDrag.transform.parent.parent.name == "inventory")
                        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().equ.equipping(end_pos, start_pos);
                    break;
                 case "inventory":
                    if (eventData.pointerDrag.transform.parent.parent.name == "equip")
                        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().equ.unequipping(start_pos, end_pos);
                    else
                        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().invent.move_item(start_pos, end_pos);
                    break;
             }
        }
    }
}
