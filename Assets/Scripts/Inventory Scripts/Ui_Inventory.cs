using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    [SerializeField] RectTransform itemSlotRectTransform;
    [SerializeField] Image image;

    private void Awake() 
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory) 
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) 
    {
        RefreshInventoryItem();
    }

    private void RefreshInventoryItem()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        //itemslotcell location
        int x = 0;
        int y = 0;

        //size of the itemslotcell
        float itemSlotCellSize = 100f;
        foreach (Item item in inventory.GetItemList())
        {
            itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            image = itemSlotRectTransform.Find("Spriteimage").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString() + "X");
            } 
            else 
            {
                uiText.SetText("");
            }
            x++;
            if(x > 4) 
            {
                x = 0;
                y--;
            }
        }
    }
}
