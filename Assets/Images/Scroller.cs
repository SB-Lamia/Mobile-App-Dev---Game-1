using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RawImage backgroundImage;
    public float xPosition;
    public float yPosition;
    public bool isScrolling;

    private void Awake()
    {
        isScrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScrolling)
        {
            backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(xPosition, yPosition) * Time.deltaTime, backgroundImage.uvRect.size);
        }
    }
}
