using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public int childIndex;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemSprite;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        // remove stuffs?
    }

    public void UserItem()
    {
        InventoryManager.Instance.ItemPressed(childIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
