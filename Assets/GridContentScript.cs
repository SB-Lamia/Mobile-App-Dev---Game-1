using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContentScript : MonoBehaviour
{
	public int rows;
	public int cols;
	public GameObject inputFieldPrefab;

	void Awake()
	{
		RectTransform parentRect = gameObject.GetComponent<RectTransform>();
		GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
		gridLayout.cellSize = new Vector2(parentRect.rect.width / cols, parentRect.rect.height / rows);

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				GameObject inputField = Instantiate(inputFieldPrefab);
				inputField.transform.SetParent(gameObject.transform, false);
			}
		}

		transform.parent.gameObject.SetActive(false);
	}
}
