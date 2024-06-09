using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData) 
    {
        Debug.Log("Clicked on ui item");
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        Debug.Log("Dropped");
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        Debug.Log("begin dragging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("WHile Dragging");
        rectTransform.anchoredPosition += eventData.delta;
    }
}
