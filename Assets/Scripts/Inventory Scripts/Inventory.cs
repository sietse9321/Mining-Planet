using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory() {
        itemList =  new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Iron, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Copper, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Titanium, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Malachite, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Malachite, amount = 1});
        Debug.Log(itemList.Count);
        Debug.Log("inventory");
    }

    public void AddItem(Item item) {
        itemList.Add(item);
    }

    public List<Item> GetItemList(){
        return itemList;
    }
}
