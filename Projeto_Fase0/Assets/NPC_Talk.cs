using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    [SerializeField]
    private MessageWritter messageWritter;
    
    private JsonParser jsonParser;
    public TextAsset textAsset;

    public GameObject panel;
    public Text textUIName;
    public Text textUIMessage;
    public Text textUIAction;
    private string method;
    private Message[] npcMessages;
    private List<Dictionary<string, string>> listDictMessages;
    private bool talkStarted = false;
    private int phase = 0;
    private bool isYesNo = false;
    private const string CLICK_TO_CONTINUE = "Press E to continue...";
    private const string CLICK_TO_YESNO = "Left Mouse to confirm...\tRight Mouse to cancel";


    private void Start()
    {
        jsonParser = new JsonParser(textAsset, gameObject.name);
        Npc npc = jsonParser.Setup();
        npcMessages = npc.messages;
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
        if (Input.GetKeyDown(KeyCode.E) && !isYesNo && talkStarted && messageWritter.finishedWriting)
        {
            foreach (Message npcMessage in npcMessages)
            {
                if (npcMessage.phase == this.phase)
                {
                    string message = npcMessage.message;
                    string npcName = gameObject.name;
                    string action = (npcMessage.yesno ? CLICK_TO_YESNO : CLICK_TO_CONTINUE);

                    if (!npcMessage.ignore)
                    {
                        WriteMessage(npcName, message, action);
                        npcMessage.ignore = true;
                        this.method = npcMessage.method;
                        this.isYesNo = npcMessage.yesno;
                        return;
                    }
                }         
            }

            // If the talk reaches this line there is nothing left to say so closes the talk
            FinishTalk();
        }
        
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && isYesNo && talkStarted && messageWritter.finishedWriting)
        {
            string className = gameObject.name.Replace(" ", "") + "Actions";
            List<object> parameters = new List<object>();
            
            bool accepted = (Input.GetMouseButtonDown(0) ? true : false);
            parameters.Add((object)accepted);
            object[] test = parameters.ToArray();

            Debug.Log(className);
            Debug.Log(method);
            Debug.Log(JsonUtility.ToJson(parameters));

            IActions actions = gameObject.GetComponent(className) as IActions;
            actions.exec(method, test);
            
            // If the talk reaches this line there is nothing left to say so closes the talk
            FinishTalk();
        }
    }

    private void FinishTalk()
    {
        talkStarted = false;
        isYesNo = false;
        panel.SetActive(false);
        WriteMessage(gameObject.name, "", CLICK_TO_CONTINUE);

        foreach (Message npcMessage in npcMessages)
        {
            if (npcMessage.phase == this.phase)
            {
                npcMessage.ignore = false;
            }
        }
    }
}
