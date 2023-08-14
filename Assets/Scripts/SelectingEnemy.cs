using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingEnemy : MonoBehaviour
{
    public bool isIncreasingTransparent = true;
    public SpriteRenderer currentSP;
    public Color currentColor;

    public IEnumerator EnemyBlinker()
    {
        gameObject.SetActive(true);
        currentSP = this.GetComponent<SpriteRenderer>();
        while (true)
        {
            Vector3 newScale;
            currentColor = currentSP.color;
            if (isIncreasingTransparent == true)
            {
                currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + 0.05f);
            }
            else
            {
                currentColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 0.05f);
            }

            currentSP.color = currentColor;

            if (currentColor.a >= 0.5f)
            {
                isIncreasingTransparent = false;
            }
            else if (currentColor.a <= 0.05f)
            {
                isIncreasingTransparent = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, Camera.main.transform.forward);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    GameObject parentObject = this.transform.parent.parent.gameObject;
                    for (int i = 0; i > parentObject.transform.childCount; i++)
                    {
                        if (this.transform.parent.gameObject.name == parentObject.transform.GetChild(i).name)
                        {
                            BattleManager.
                        }
                    }
                }
            }
        }
    }
}
