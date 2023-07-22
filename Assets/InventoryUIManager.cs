using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public List<GameObject> disabledHud;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryControl()
    {
        if (GameManager.instance.isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        inventoryMenu.gameObject.SetActive(false);
        foreach (GameObject hud in disabledHud)
        {
            hud.SetActive(true);
        }
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    public void Pause()
    {
        inventoryMenu.gameObject.SetActive(true);
        foreach(GameObject hud in disabledHud)
        {
            hud.SetActive(false);
        }
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
        GameManager.instance.DisplayItems();
    }
}
