using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Empty,
        Copper,
        Iron,
        Malachite,
        Stone,
        Titanium
    }

    public ItemType itemType = ItemType.Empty;
    public int amount;
    public int Location;
    public Sprite oreSprite;

    public Item(ItemType pItemtype, int pAmount)
    {
        itemType = pItemtype;
        amount = pAmount;
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Iron:
            case ItemType.Copper:
            case ItemType.Stone:
                return true;
            case ItemType.Malachite:
            case ItemType.Titanium:
            case ItemType.Empty:
                return false;
        }
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Iron: return ItemAssets.Instance.ironSprite;
            case ItemType.Copper: return ItemAssets.Instance.copperSprite;
            case ItemType.Malachite: return ItemAssets.Instance.malachiteSprite;
            case ItemType.Titanium: return ItemAssets.Instance.titaniumSprite;
            case ItemType.Stone: return ItemAssets.Instance.stoneSprite;
            case ItemType.Empty: return null;
        }
    }

}