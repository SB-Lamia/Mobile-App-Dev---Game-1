using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public List<GameObject> disabledHud;

    public Transform itemsParent;
    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.gameObject.SetActive(false);
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
