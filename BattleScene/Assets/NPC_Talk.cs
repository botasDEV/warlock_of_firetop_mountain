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
    private GameObject camera;
    public Npc npc;
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


    private void Awake()
    {
        camera = GameObject.Find("Main Camera");
        Debug.Log(camera);
        jsonParser = new JsonParser(textAsset, gameObject.name);
        npc = jsonParser.Setup();
        npcMessages = npc.messages;
    }

    private void Start()
    {
        // Start talk on the script start
        talkStarted = true;
        panel.SetActive(true);
        npcMessages[0].ignore = true;
        string npcMessage = npcMessages[0].message;

        string action = CLICK_TO_CONTINUE;
        string npcName = gameObject.name;
        WriteMessage(npcName, npcMessage, action);
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
            if (!accepted)
            {
                foreach (Message npcMessage in npcMessages)
                {
                    int phaseAux = (phase * -10 == 0 ? -11 : phase * -10);

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
            } else
            {
                // Call the method dynamically
                string className = gameObject.name.Replace(" ", "").Replace("'", "") + "Actions";
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
    }

    private void FinishTalk()
    {
        talkStarted = false;
        isYesNo = false;
        panel.SetActive(false);
        camera.GetComponent<PlayerManagement>().canPlay = true;
        WriteMessage(gameObject.name, "", CLICK_TO_CONTINUE);

        foreach (Message npcMessage in npcMessages)
        {
            if (npcMessage.phase == phase)
            {
                npcMessage.ignore = false;
            }
        }
    }
}
