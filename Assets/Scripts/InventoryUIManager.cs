using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public List<GameObject> disabledHud; 
    public static InventoryUIManager instance;
    public Transform itemsParent;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Resume()
    {
        inventoryMenu.gameObject.SetActive(false);
        GameManager.instance.ToggleDefaultHud(true);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    public void Pause()
    {
        inventoryMenu.gameObject.SetActive(true);
        GameManager.instance.ToggleDefaultHud(false);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
        GameManager.instance.DisplayItems();
    }
}
