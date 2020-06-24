using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{
    [SerializeField]
    private MessageWritter messageWritter;

    public GameObject gameStates;
    
    private JsonParser jsonParser;
    public TextAsset textAsset;

    public GameObject panel;
    public Npc npc;
    public Text textUIName;
    public Text textUIMessage;
    public Text textUIAction;
    private string method;
    private Message[] npcMessages;
    private List<Dictionary<string, string>> listDictMessages;
    public bool talkStarted = false;
    public int phase = 0;    
    private bool isYesNo = false;
    public const string CLICK_TO_CONTINUE = "Press E to continue...";
    public const string CLICK_TO_YESNO = "Left Mouse to confirm...\tRight Mouse to cancel";


    private void Awake()
    {
        jsonParser = new JsonParser(textAsset, gameObject.name);
        npc = jsonParser.Setup();
        npcMessages = npc.messages;
    }

    private void Start()
    {
        // Start talk on the script start
        panel.SetActive(true);
        
        npcMessages[0].ignore = true;
        string npcMessage = npcMessages[0].message;

        string action = CLICK_TO_CONTINUE;
        string npcName = gameObject.name;
        if (gameStates.GetComponent<StatesScript>().state == GameStates.START)
        {
            talkStarted = true;
            WriteMessage(npcName, npcMessage, action);
        }
        
    }

    public void WriteMessage(string npcName, string message, string action)
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
                if (npcMessage.phase == phase)
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
            bool accepted = (Input.GetMouseButtonDown(0) ? true : false);
            
            // Call the method dynamically
            string className = "NPC_Manage";
            List<object> parameters = new List<object>();
            parameters.Add((bool)accepted);
            IActions actions = gameObject.GetComponent(className) as IActions;

            actions.exec(method, parameters.ToArray());
                
            int phaseAux = (phase * 10 == 0 ? 11 : phase * 10);
            foreach (Message npcMessage in npcMessages)
            {
                if (npcMessage.phase == phaseAux)
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
    }

    private void FinishTalk()
    {
        talkStarted = false;
        isYesNo = false;
        panel.SetActive(false);
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;
        
        gameStates.GetComponent<StatesScript>().state = (actualState == GameStates.START ?
            GameStates.PLAYERTURN :
            actualState == GameStates.PLAYERTURN ?
            GameStates.PLAYERMAINPHASE :
            actualState);

        WriteMessage(gameObject.name, "", CLICK_TO_CONTINUE);

        foreach (Message npcMessage in npcMessages)
        {
            if (npcMessage.phase == phase)
            {
                npcMessage.ignore = false;
            }
        }
    }

    public void StartConversation(int newPhase, string action = CLICK_TO_CONTINUE)
    {
        Message message = null;
        phase = newPhase;

        foreach (Message msg in npcMessages)
        {
            if (msg.phase == phase)
            {
                message = msg;
                msg.ignore = true;
                break;
            }
        }
        panel.SetActive(true);
        
        talkStarted = true;
        method = message.method;
        isYesNo = message.yesno;
        WriteMessage(gameObject.name, message.message, action);
    }
}
