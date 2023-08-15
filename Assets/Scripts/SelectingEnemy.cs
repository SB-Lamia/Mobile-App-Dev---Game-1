using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

            if (currentColor.a >= 1f)
            {
                isIncreasingTransparent = false;
            }
            else if (currentColor.a <= 0.7f)
            {
                isIncreasingTransparent = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
