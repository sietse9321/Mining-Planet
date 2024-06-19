using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IDropHandler
{

    private RectTransform rectTransform;
    private Vector3 itemslotposition;
    private Image image;
    private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Item itemInSlot;
    [SerializeField] ItemSlot tempItemSlot;
    [SerializeField] int amountInSlot;
    //public string itemType;

    public Item.ItemType debugItemtype;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        image = GetComponent<Image>();

        canvasGroup = GetComponent<CanvasGroup>();

        rectTransform = GetComponentInChildren<RectTransform>();

        itemslotposition = GetComponentInChildren<RectTransform>().localPosition;

        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void GetItemInside(Item pItem)
    {
        itemInSlot = pItem;
        //itemType = itemInSlot.itemType.ToString();
        amountInSlot = itemInSlot.amount;
    }

    public void SetItemslotSprite(Sprite newSprite, Item.ItemType pItemType)
    {
        image.sprite = newSprite;
        debugItemtype = pItemType;
        TransparentIfEmpty();
    }

    public void SetItemslotSprite()
    {
        image.sprite = null;
        TransparentIfEmpty();
    }

    public void SetItemslotText(int pAmount)
    {
        if (pAmount == 0)
        {
            textMeshProUGUI.SetText("");
        }
        else
        {
            textMeshProUGUI.SetText(pAmount.ToString() + "x");
        }
    }

    public void TransparentIfEmpty()
    {

        if (image.sprite != null)
        {
            image.color = Color.white;
        }
        else
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = itemslotposition;
        //canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;

        tempItemSlot = GetComponent<ItemSlot>();
        itemInSlot = null;
        //Debug.Log("old itemslot: " + oldItemSlot.itemInSlot.amount);
        //Debug.Log("old itemslot: " + oldItemSlot.itemInSlot.itemType);

        if (eventData.pointerDrag != null)
        {   
            
            ItemSlot tempItemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
            //Debug.Log("new itemslot: " + tempItemSlot.itemInSlot.itemType);
            //Debug.Log("new itemslot: " + tempItemSlot.itemInSlot.amount);
            
            SetItemslotSprite(null, Item.ItemType.Empty);
            SetItemslotText(0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {   
            ItemSlot tempItemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
            //Debug.Log("new itemslot: " + tempItemSlot.itemInSlot.itemType);
            //Debug.Log("new itemslot: " + tempItemSlot.itemInSlot.amount);
            
            SetItemslotSprite(tempItemSlot.tempItemSlot.itemInSlot.GetSprite(), tempItemSlot.tempItemSlot.itemInSlot.itemType);
            SetItemslotText(tempItemSlot.tempItemSlot.itemInSlot.amount);
            //itemInSlot = null;
        }
        //oldItemSlot
    }
}
