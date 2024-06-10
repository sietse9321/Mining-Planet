using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType { Copper, Iron, Malachite, Stone, Titanium }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() 
    {
        switch (itemType) 
        {
            default:
            case ItemType.Iron:         return ItemAssets.Instance.ironSprite;
            case ItemType.Copper:       return ItemAssets.Instance.copperSprite;
            case ItemType.Malachite:    return ItemAssets.Instance.malachiteSprite;
            case ItemType.Titanium:     return ItemAssets.Instance.titaniumSprite;
            case ItemType.Stone:        return ItemAssets.Instance.stoneSprite;
        }
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
                return false;
        }
    }
}
