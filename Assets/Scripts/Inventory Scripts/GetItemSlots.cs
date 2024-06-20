using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GetItemSlots : MonoBehaviour
{

    ItemSlot[] tempItemSlots = {};
    public List<ItemSlot> itemSlots = new ();


    void Start()
    {
        tempItemSlots = GetComponentsInChildren<ItemSlot>();

        foreach(ItemSlot itemslot in tempItemSlots) 
        {
            itemSlots.Add(itemslot);
        }
    }

    public void ChangeImage(int pIndex, Sprite pSprite, Item.ItemType pItemtype) 
    {
        itemSlots[pIndex].SetItemslotSprite(pSprite, pItemtype);
    }

    public void ChangeText(int pIndex, int pAmount)
    {
        itemSlots[pIndex].SetItemslotText(pAmount);
    }
    
    
    
    
    /*private GameObject[] itemSlots = { };
    private Image[] itemSlotsImagesArray = { };
    public List<Image> itemSlotsImages = new();

    public TextMeshProUGUI[] itemSlotText = { };

    void Start()
    {
        //itemSlots = GetComponentsInChildren<RectTransform>();
        itemSlotsImagesArray = GetComponentsInChildren<Image>();
        foreach (Image image in itemSlotsImagesArray)
        {
            itemSlotsImages.Add(image);
        }
        itemSlotsImages.Remove(itemSlotsImages[0]);
        itemSlotsImages.Remove(itemSlotsImages[0]);


        itemSlotText = GetComponentsInChildren<TextMeshProUGUI>();

        TransparentIfEmpty();
    }

    public void ChangeItemslotSprite(int pIndex, Sprite newSprite)
    {
        itemSlotsImages[pIndex].sprite = newSprite;
        TransparentIfEmpty();
    }

    public void ChangeItemslotText(int pIndex, int pAmount)
    {
        if (pAmount == 0)
        {
            itemSlotText[pIndex].SetText("");
        }
        else
        {
            itemSlotText[pIndex].SetText(pAmount.ToString() + "x");
        }
    }

    public void TransparentIfEmpty()
    {
        foreach (Image image in itemSlotsImages)
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

    }*/

}
