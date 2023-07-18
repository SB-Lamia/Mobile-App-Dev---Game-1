using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public const float PlayerSpeed = 3f;
    private GameObject CurrentCityMovement;
    private bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MovePlayer();
        }
        else
        {
            CheckToMove();
        }
    }

    private void CheckToMove()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, Camera.main.transform.forward);

            if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    GameObject touchedObject = hit.transform.gameObject;

                    CheckObjectTouched(touchedObject);
                }
            }
        }
    }

    private void CheckObjectTouched(GameObject touchedGameobject)
    {
        switch (touchedGameobject.name)
        {
            case "City":
                TriggerMovement(touchedGameobject);
                break;
            default:
                Debug.Log("Nothing Touched");
                break;
        }
    }

    private void TriggerMovement(GameObject touchedGameObject)
    {
        CurrentCityMovement = touchedGameObject;
        isMoving = true;
    }

    private void MovePlayer()
    {
        float step = PlayerSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, CurrentCityMovement.transform.position, step);
        if(transform.position == CurrentCityMovement.transform.position)
        {
            CurrentCityMovement = null;
            isMoving = false;
        }
    }
}
