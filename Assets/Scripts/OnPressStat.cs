using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPressStat : MonoBehaviour
{
    private int layerUI;

    void Start()
    {
        layerUI = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, Camera.main.transform.forward);

            if (hit.collider != null)
            {
                if (hit.transform.gameObject.layer == layerUI)
                {
                    //Do Nothing
                }
            }
        }
    }
}
