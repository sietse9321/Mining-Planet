using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new();
    public string owner;

    public int amountInItems;
    [SerializeField] GetItemSlots itemSlots;

    [SerializeField] int pIndexT = 1;
    [SerializeField] Sprite spriteT;

    void Start()
    {
        owner = gameObject.name;
        //items.Add(new Item (Item.ItemType.Iron, 2));
        //spriteT = items[0].GetSprite();
    }

    void Update()
    {
        amountInItems = items.Count;
    }

    public void AddItem(Item pNewItem)
    {

        if (pNewItem.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item item in items)
            {
                if (item.itemType == pNewItem.itemType)
                {
                    item.amount += pNewItem.amount;
                    itemSlots.ChangeText(items.IndexOf(item), item.amount);
                    itemSlots.itemSlots[items.IndexOf(item)].GetItemInside(item);
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory && items.Count < 18)
            {
                items.Add(pNewItem);
                itemSlots.itemSlots[items.IndexOf(pNewItem)].GetItemInside(pNewItem);
                //itemSlots.itemSlots[items.IndexOf(pNewItem)].GetItemInside(pNewItem);
                Debug.Log("added " + pNewItem.itemType);
                itemSlots.ChangeImage(items.IndexOf(pNewItem), 
                pNewItem.GetSprite(), 
                pNewItem.itemType);
            }
        }
        else
        {
            if (items.Count < 18)
            {
                items.Add(pNewItem);
                itemSlots.itemSlots[items.IndexOf(pNewItem)].GetItemInside(pNewItem);
                Debug.Log("added " + pNewItem.itemType);
                itemSlots.ChangeImage(items.IndexOf(pNewItem), pNewItem.GetSprite(), pNewItem.itemType);
            }

        }
    }
    public List<Item> GetItemList()
    {
        return items;
    }
}
