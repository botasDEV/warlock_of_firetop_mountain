using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWritter : MonoBehaviour
{
    private Text uiTextMessage;
    private Text uiTextName;
    private Text uiTextAction;
    private string textToWrite;
    private int characterIndex;
    private float characterTime;
    private float timer;
    public bool finishedWriting = false;

    
    public void AddWriter(Text textName, Text textMessage, Text textAction, string message, float charTime)
    {
        this.uiTextName = textName;
        this.uiTextMessage = textMessage;
        this.uiTextAction = textAction;
        this.textToWrite = message;
        this.characterTime = charTime;

        characterIndex = 0;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (uiTextMessage != null)
        {
            textToWrite = ReplaceSpecials();
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                finishedWriting = false;
                timer += characterTime;
                characterIndex++;

                if (string.IsNullOrEmpty(textToWrite))
                {
                    uiTextMessage.text = "";
                }

                if (characterIndex >= textToWrite.Length)
                {
                    finishedWriting = true;
                    uiTextMessage = null;
                    return;
                }

                uiTextMessage.text = textToWrite.Substring(0, characterIndex);
            }
        }
    }

    private string ReplaceSpecials()
    {
        textToWrite = textToWrite.Replace("\n", "\n");
        textToWrite = textToWrite.Replace("\\n", "\n");
        textToWrite = textToWrite.Replace("\r", "\r");
        textToWrite = textToWrite.Replace("\\r", "\r");
        textToWrite = textToWrite.Replace("\t", "\t");
        textToWrite = textToWrite.Replace("\\t", "\t");

        return textToWrite;
    }
}
