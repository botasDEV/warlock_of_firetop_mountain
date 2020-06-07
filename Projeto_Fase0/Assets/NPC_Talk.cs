using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    [SerializeField]
    private MessageWritter messageWritter;
    public GameObject panel;
    public Text textUIName;
    public Text textUIMessage;
    public Text textUIAction;
    private List<string> npcMessages;
    private List<Dictionary<string, string>> listDictMessages;
    private bool talkStarted = false;
    private const string CLICK_TO_CONTINUE = "Press E to continue...";
    private const string DICTIONARY_KEY_MESSAGE = "message";
    private const string DICTIONARY_KEY_IGNORE = "ignore";

    private void Start()
    {
        listDictMessages = new List<Dictionary<string, string>>();

        npcMessages = gameObject.GetComponent<Npc_Messages>().messages;
        foreach(string message in npcMessages)
        {
            Dictionary<string, string> auxDictionary = new Dictionary<string, string>
            {
                { DICTIONARY_KEY_MESSAGE, message },
                { DICTIONARY_KEY_IGNORE, "false" }
            };

            listDictMessages.Add(auxDictionary);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            talkStarted = true;
            panel.SetActive(true);
            string npcMessage = "";
            string action = CLICK_TO_CONTINUE;
            string npcName = gameObject.name;
            WriteMessage(npcName, npcMessage, action);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FinishTalk();
        }
    }

    private void WriteMessage(string npcName, string message, string action)
    {
        textUIName.text = npcName;
        textUIAction.text = action;
        messageWritter.AddWriter(textUIName, textUIMessage, textUIAction, message, .01f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && talkStarted && messageWritter.finishedWriting)
        {
            foreach (Dictionary<string, string> dictMessages in listDictMessages)
            {
                string npcMessage = dictMessages[DICTIONARY_KEY_MESSAGE];
                string npcName = gameObject.name;
                string action = CLICK_TO_CONTINUE;

                if (!bool.Parse(dictMessages[DICTIONARY_KEY_IGNORE]))
                {
                    WriteMessage(npcName, npcMessage, action);
                    dictMessages[DICTIONARY_KEY_IGNORE] = "true";
                    return;
                }                
            }

            // If the talk reaches this line there is nothing left to say so closes the talk
            FinishTalk();
        }
    }

    private void FinishTalk()
    {
        talkStarted = false;
        panel.SetActive(false);
        WriteMessage(gameObject.name, "", CLICK_TO_CONTINUE);

        foreach (Dictionary<string, string> dictMessages in listDictMessages)
        {
            dictMessages[DICTIONARY_KEY_IGNORE] = "false";
        }
    }
}
