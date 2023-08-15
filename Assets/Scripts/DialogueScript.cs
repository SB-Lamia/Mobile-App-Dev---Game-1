using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    //Core TextBox stuff
    public TextMeshProUGUI textBox;
    private string currentTextBoxInput = "";
    private char[] currentTextBoxChar = new char[0];
    private int countChar = 0;
    private float typingTime = 0;
    private float skippingTime = 0;
    private bool startTyping = false;
    private bool canSkip = false;
    private bool endDialogue = false;
    private bool canTouch = false;
    private bool canType = false;
    public bool dialogueEnded = false;

    //Call this method to start the dialogue typing
    public void ResetString(string text)
    {
        textBox = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        currentTextBoxChar = text.ToCharArray();
        // Reseting scale and settings of textbox incase it breaks

        RectTransform textGO = textBox.rectTransform;
        textGO.localScale = new Vector3(1f, 1f, 1f);
        RectTransformExtensions.SetLeft(textGO, 0f);
        RectTransformExtensions.SetRight(textGO, 0f);
        RectTransformExtensions.SetTop(textGO, 0f);
        RectTransformExtensions.SetBottom(textGO, 0f);
        textGO.localPosition = new Vector3(textGO.localPosition.x, textGO.localPosition.y, 0f);
        canType = true;
        startTyping = true;
    }

    void Update()
    {
        if (canType)
        {
            Debug.Log("Typing");
            Skipping();
            Typing();
            if (endDialogue && canSkip)
            {
                Debug.Log("DialogueEnded");
                dialogueEnded = true;
            }
            else
            {
                Debug.Log("Continuing Dialogue");
            }
        }
        
    }


    private void Skipping()
    {
        Debug.Log("Skipping");
        skippingTime += Time.deltaTime;
        if (skippingTime >= 0.3f && canTouch)
        {
            Debug.Log("Skipping Allowed");
            skippingTime = 0.0f;
            canTouch = true;
        }
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began &&
            canTouch)
        {
            Debug.Log("Text Skipped");
            canSkip = true;
        }
    }

    private void Typing()
    {
        Debug.Log("IsTyping");
        if (startTyping)
        {
            typingTime += Time.deltaTime;

            if (typingTime >= 0.01f)
            {
                typingTime = 0.0f;

                currentTextBoxInput = currentTextBoxInput + currentTextBoxChar[countChar];

                if (countChar >= currentTextBoxChar.Length - 1)
                {
                    startTyping = false;
                }
                else
                {
                    countChar++;
                }

                textBox.text = currentTextBoxInput;
            }
        }
        if (canSkip && startTyping)
        {
            startTyping = false;
            canSkip = false;
        }
    }
}
