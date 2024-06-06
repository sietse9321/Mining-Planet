using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Copper,
        Iron,
        Malachite,
        Stone, 
        Titanium
    }

    public ItemType itemType;
    public int amount;
}
