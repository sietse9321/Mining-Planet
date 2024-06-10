using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Inventory inventory;
    Transform storage;
    [SerializeField] Transform ui_inventoryBackground;
    [SerializeField] private Ui_Inventory uiInventory;

    bool hovering;
    void Start()
    {
        storage = GetComponent<Transform>();
        //ui_inventoryBackground = storage.Find("Background").GetComponent<Transform>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    void Update() 
    {
        OpenStorageUi();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering " + gameObject.name);
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exiting " + gameObject.name);
        hovering = false;
    }

    public void OpenStorageUi() 
    {
        if (hovering == true && Input.GetKey(KeyCode.Mouse1))
        {
            ui_inventoryBackground.gameObject.SetActive(true);
        } else if (hovering == false && Input.GetKey(KeyCode.Mouse1)) 
        {
            ui_inventoryBackground.gameObject.SetActive(false);
        }
    }
}
