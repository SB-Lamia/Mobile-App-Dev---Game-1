using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContentScript : MonoBehaviour
{
	public int rows;
	public int cols;
	public GameObject inputFieldPrefab;
	public int childPosition;

	void Awake()
	{
		childPosition = 0;
		RectTransform parentRect = gameObject.GetComponent<RectTransform>();
		GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
		gridLayout.cellSize = new Vector2(parentRect.rect.width / cols, parentRect.rect.height / rows);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				GameObject inputField = Instantiate(inputFieldPrefab);
				inputField.transform.SetParent(gameObject.transform, false);
				inputField.transform.GetComponent<InventorySlot>().childIndex = childPosition;
				childPosition++;
                if (this.gameObject.transform.parent.name == "TraderItems" || this.gameObject.transform.parent.name == "PlayerItems")
                {
					inputField.transform.GetComponent<InventorySlot>().inventoryLocation = InventorySlot.InventoryLocation.Trader;
                }
                else
                {
					inputField.transform.GetComponent<InventorySlot>().inventoryLocation = InventorySlot.InventoryLocation.InventorySystem;
				}
			}
		}
	}
}
