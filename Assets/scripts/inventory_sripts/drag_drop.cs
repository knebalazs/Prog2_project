using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag_drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rect;
    public Vector2 begin_pos;
    [SerializeField] CanvasGroup canv;


    void Start()
    {
        rect = GetComponent<RectTransform>();
        canv = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        begin_pos = this.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canv.alpha = 0.6f;
        canv.blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rect.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canv.alpha = 1f;
        canv.blocksRaycasts = true;
        this.transform.position = begin_pos;
    }
}
