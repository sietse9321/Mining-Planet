using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    [SerializeField] RectTransform itemSlotRectTransform;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    private void Awake() {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory) 
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItem();
    }

    private void RefreshInventoryItem(){
        foreach(Transform child in itemSlotContainer){
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;
        foreach (Item item in inventory.GetItemList()){
            itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            image = itemSlotRectTransform.Find("Spriteimage").GetComponent<Image>();
            image.sprite = item.GetSprite();

            text = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            text.SetText(item.amount.ToString());
            if (item.amount > 1)
            text.SetText(item.amount.ToString());
            else 
            text.SetText("");      
            x++;
            if(x > 4) {
                x = 0;
                y--;
            }
        }
    }
}
