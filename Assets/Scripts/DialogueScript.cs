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
    public bool dialogueEnded = false;

    //Call this method to start the dialogue typing
    public void ResetString(string text)
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
        currentTextBoxChar = text.ToCharArray();
        StartCoroutine(DialogueCoroutine());
    }

    public IEnumerator DialogueCoroutine()
    {
        Skipping();
        Typing();
        if (endDialogue && canSkip)
        {
            dialogueEnded = true;
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(.01f);
        }
    }

    private void Skipping()
    {
        skippingTime += Time.deltaTime;
        if (skippingTime >= 0.3f && canTouch)
        {
            skippingTime = 0.0f;
            canTouch = true;
        }
        if (Input.touchCount == 1 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began &&
            canTouch)
        {
            canSkip = true;
        }
    }

    private void Typing()
    {
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
