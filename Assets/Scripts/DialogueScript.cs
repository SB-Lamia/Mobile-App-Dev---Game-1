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
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
        currentTextBoxInput = "";
        // Reseting scale and settings of textbox incase it breaks

        RectTransform textGO = textBox.rectTransform;
        textGO.localScale = new Vector3(1f, 1f, 1f);
        RectTransformExtensions.SetLeft(textGO, 0f);
        RectTransformExtensions.SetRight(textGO, 0f);
        RectTransformExtensions.SetTop(textGO, 0f);
        RectTransformExtensions.SetBottom(textGO, 0f);
        textBox.horizontalAlignment = HorizontalAlignmentOptions.Center;    
        textBox.verticalAlignment = VerticalAlignmentOptions.Middle;
        textGO.localPosition = new Vector3(textGO.localPosition.x, textGO.localPosition.y, 0f);
        currentTextBoxChar = text.ToCharArray();
        canType = true;
        startTyping = true;
        countChar = 0;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (canType)
        {
            Skipping();
            Typing();
            if (endDialogue && canSkip)
            {
                dialogueEnded = true;
            }
        }
        
    }


    private void Skipping()
    {
        skippingTime += Time.deltaTime;
        if (skippingTime >= 3f)
        {
            skippingTime = 0.0f;
            canTouch = true;
        }
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began &&
            canTouch)
        {
            canSkip = true;
            endDialogue = true;
        }
    }

    private void Typing()
    {
        if (startTyping)
        {
            typingTime += Time.deltaTime;

            if (typingTime >= 0.1f)
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
            
        }
    }
}
