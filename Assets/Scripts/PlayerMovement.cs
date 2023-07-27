using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public const float PlayerSpeed = 3f;
    public GameObject popupMenu;

    private GameObject CurrentCityMovement;
    private bool isMoving = false;
    private GameObject touchedObject;
    private bool popupMenuOpen = false;

    private bool isIncreasingScale = true;
    private Transform cityBorderBlinker;

    private bool firstMovement = true;

    private float movementCost;
    private int layerUI;

    private void Awake()
    {
        layerUI = LayerMask.NameToLayer("UI");
    }

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
                else if (hit.collider.CompareTag("City"))
                {
                    touchedObject = hit.transform.gameObject;

                    if (GameManager.instance.isPaused == false)
                    {
                        CheckObjectTouched();
                    }
                    else
                    {
                        Debug.Log("Error: Menu Already Opened.");
                    }
                }
            }
        }
    }

    private void CheckObjectTouched()
    {
        switch (touchedObject.name)
        {
            case "City(Clone)":
                askPlayerIfMoving();
                break;
            default:
                break;
        }
    }

    private void askPlayerIfMoving()
    {
        popupMenu.SetActive(true);
        popupMenuOpen = true;
        cityBorderBlinker = touchedObject.transform.GetChild(1).transform;
        if (firstMovement)
        {
            movementCost = -15;
        }
        else
        {
            movementCost = (Vector2.Distance(CityManager.instance.currentCity.transform.position, touchedObject.transform.position) / 2) * -1;
        }
        popupMenu.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Food / Water Cost: " + Mathf.Round(movementCost * -1);
        StartCoroutine("cityBlinker");
    }

    public void playerConfirmedMovementYes()
    {
        firstMovement = false;
        TriggerMovement();
        StatBarManager.instance.UpdateHunger(movementCost);
        StatBarManager.instance.UpdateWater(movementCost);
        CityManager.instance.currentCity = touchedObject;
        CityManager.instance.openCityButton.interactable = false;
        popupMenu.SetActive(false);
        popupMenuOpen = false;
    }

    public void playerConfirmedMovementNo()
    {
        StopCoroutine("cityBlinker");
        touchedObject = null;
        popupMenu.SetActive(false);
        popupMenuOpen = false;
    }

    private IEnumerator cityBlinker()
    {
        while (true)
        {
            Vector3 newScale;

            if (isIncreasingScale == true)
            {
                newScale = new Vector3(cityBorderBlinker.localScale.x + 0.05f, cityBorderBlinker.localScale.y + 0.05f, 1);
            }
            else
            {
                newScale = new Vector3(cityBorderBlinker.localScale.x - 0.05f, cityBorderBlinker.localScale.y - 0.05f, 1);
            }

            cityBorderBlinker.localScale = newScale;

            if (cityBorderBlinker.localScale.x >= 1.2f)
            {
                isIncreasingScale = false;
            }
            else if (cityBorderBlinker.localScale.x <= 1.15f)
            {
                isIncreasingScale = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void TriggerMovement()
    {
        CurrentCityMovement = touchedObject;
        isMoving = true;
    }

    private void MovePlayer()
    {
        float step = PlayerSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, CurrentCityMovement.transform.position, step);
        if (transform.position == CurrentCityMovement.transform.position)
        {
            CurrentCityMovement = null;
            isMoving = false;
            StopCoroutine("cityBlinker");
            CityManager.instance.openCityButton.interactable = true;
        }
    }

}
