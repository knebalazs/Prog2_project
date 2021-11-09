using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class equip_slot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        slot slot_ref = FindObjectOfType<slot>();
        int start_pos = slot_ref.find_pos(eventData.pointerDrag.transform.parent.gameObject);
        int end_pos;
        if (this.transform.parent.GetChild(0).gameObject == this.gameObject)
            end_pos = 0;
        else
            end_pos = 1;


        
        if (eventData.pointerDrag != null && !this.transform.GetChild(0).gameObject.activeSelf)
        {
            eventData.pointerDrag.transform.position = eventData.pointerDrag.GetComponent<drag_drop>().begin_pos;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
            GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().equ.equipping(end_pos, start_pos);
        }
    }
}
