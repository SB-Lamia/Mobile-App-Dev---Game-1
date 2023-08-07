using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class DialogueScript : MonoBehaviour
{
    //Core TextBox stuff
    public TextMeshProUGUI textBox;
    private string currentTextBoxInput;
    private char[] currentTextBoxChar;
    private int countChar = 0;
    private float typingTime = 0;
    private float skippingTime = 0;
    private bool startTyping = false;
    private bool doneTyping = false;
    private bool canSkip = false;
    private bool endTutorialChecker = false;
    public bool canClick = false;

    //OptionalStrings
    public string enemyName;
    public string enemyActionInfo;
    public string enemyNextAction;

    public void ResetString(string enemyName, string actionInfo, string nextAction)
    {
        this.enemyName = enemyName;
        this.enemyActionInfo = actionInfo;
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            Debug.Log("Can Skip now");
            canSkip = true;
            canClick = false;
        }
    }

    void Update()
    {
        skippingTime += Time.deltaTime;
        if (skippingTime >= 0.3f && canClick == false)
        {
            skippingTime = 0.0f;
            canClick = true;
        }

        if (endTutorialChecker == true && canSkip)
        {
            Destroy(this.gameObject);
        }
        CheckDoneTyping();
        Typing();
    }

    public void CheckDoneTyping()
    {
        if (doneTyping == true)
        {
            Debug.Log("DoneTyping");
            
        }
    }


    public void Typing()
    {
        if (startTyping == true)
        {
            typingTime += Time.deltaTime;

            if (typingTime >= 0.05f)
            {
                typingTime = 0.0f;

                currentTextBoxInput = currentTextBoxInput + currentTextBoxChar[countChar];

                if (countChar >= currentTextBoxChar.Length - 1)
                {
                    countChar = 0;
                    startTyping = false;
                    doneTyping = true;
                }
                else
                {
                    countChar++;
                }

                UpdateTextBox();
            }
        }
        if (canSkip == true && startTyping == true)
        {
            Debug.Log("SkippedText");
            startTyping = false;
            countChar = 0;
            doneTyping = true;
            canSkip = false;
            UpdateTextBox();
        }
    }
    public void UpdateTextBox()
    {
        textBox.text = currentTextBoxInput;
    }
}
