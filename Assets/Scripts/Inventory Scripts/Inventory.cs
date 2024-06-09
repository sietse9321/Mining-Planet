using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;

    private List<Item> itemList;

    public Inventory() {
        itemList =  new List<Item>();

        Debug.Log(itemList.Count);
        Debug.Log("inventory");
        AddItem(new Item{ itemType = Item.ItemType.Iron, amount = 10} );
    }

    public void AddItem(Item item) {
        if (item.isStackable()){
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) {
                itemList.Add(item);
            }
        } else {
            itemList.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList(){
        return itemList;
    }
}
