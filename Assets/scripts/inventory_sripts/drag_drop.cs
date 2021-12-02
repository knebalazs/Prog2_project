using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag_drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rect;
    public Vector2 begin_pos;
    slot slot_ref;
    [SerializeField] CanvasGroup canv;
    private item_sprites icon_sprites;


    void Start()
    {
        rect = GetComponent<RectTransform>();
        canv = GetComponent<CanvasGroup>();
        icon_sprites = GameObject.FindGameObjectWithTag("icons").GetComponent<item_sprites>();
        slot_ref = new slot();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        begin_pos = this.transform.position;

        if (GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().invent.item_list[slot_ref.find_pos(this.gameObject.transform.parent.gameObject)].item_type == my_item.Item_type.fish)
        {
            GameObject.Find("player").GetComponent<stat_controller>().hunger += 10;
            GameObject.Find("player").GetComponent<stat_controller>().health += 10;
            GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().invent.remove_item(slot_ref.find_pos(this.gameObject.transform.parent.gameObject));
            GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().ui_inventory.refresh_inventory();
        }

        if (GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().invent.item_list[slot_ref.find_pos(this.gameObject.transform.parent.gameObject)].item_type == my_item.Item_type.bottle)
        {
            if (GameObject.Find("player").GetComponent<stat_controller>().bottle_contains > 0)
            {
                if (GameObject.Find("player").GetComponent<stat_controller>().thirst + 20 < 100)
                    GameObject.Find("player").GetComponent<stat_controller>().thirst += 40;
                else
                    GameObject.Find("player").GetComponent<stat_controller>().thirst = 100;
                GameObject.Find("player").GetComponent<stat_controller>().bottle_contains--;
                GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().ui_inventory.refresh_inventory();
            }
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canv.alpha = 0.6f;
        canv.blocksRaycasts = false;
        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().is_draging_item = true;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rect.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().is_draging_item = false;
        canv.alpha = 1f;
        canv.blocksRaycasts = true;
        this.transform.position = begin_pos;
    }
}
