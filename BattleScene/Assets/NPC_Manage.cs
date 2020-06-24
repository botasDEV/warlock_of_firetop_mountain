using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manage : MonoBehaviour, IActions
{
    private Npc npc;

    public GameObject gameStates;
    public GameObject foeDices;
    public GameObject player;
    public Text playerDamage;
    public Text foeDamage;
    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    
    int actualPhase = -1;
    bool foeDiceRolled = false;
    public bool explainedLuck = false;
    bool hasFinishedLuckText = false; 

    public int currentStrength;
    int currentExpertise;
    int currentLuck;



    // Start is called before the first frame update
    void Start()
    {
        npc = gameObject.GetComponent<NPC_Talk>().npc;

        currentStrength = npc.stats.strength;
        currentExpertise = npc.stats.expertise;
        currentLuck = npc.stats.luck;
    }

    private void Update()
    {
        // Get the actual state
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;

        // When in the FOE's TURN roll it's dice
        if (actualState == GameStates.FOETURN)
        {
            RollDice();
        }

        // Triggers a foes reaction for the player's roll
        if (actualState == GameStates.PLAYERMAINPHASE)
        {
            StartCoroutine(ReactToPlayer());
        }

        // Updates the NPC Stats whenever they change
        string strengthTxt = currentStrength + " / " + npc.stats.strength;
        string expertiseTxt = currentExpertise + " / " + npc.stats.expertise;
        string luckTxt = currentLuck + " / " + npc.stats.luck;
        if (!strengthText.text.Equals(strengthTxt) || !expertiseText.text.Equals(expertiseTxt) || !luckText.text.Equals(luckTxt))
        {
            WriteStats(strengthTxt, expertiseTxt, luckTxt);
        }

        // Resets the Foe's dice to the initial position
        if (actualState == GameStates.FOETURN && foeDamage.text != "0")
        {
            foeDices.GetComponent<DiceScript>().ResetDice();
            gameStates.GetComponent<StatesScript>().state = GameStates.MAINPHASE;
            foeDiceRolled = false;
        }

        if (actualState == GameStates.LUCK && !hasFinishedLuckText)
        {
            StartCoroutine(IntroduceLuck());
        }
    }

    IEnumerator ReactToPlayer()
    {
        bool talkEnded = false;
        int phase = (int.Parse(playerDamage.text) > 6 ? 10 : 11);
        bool talkStarted = gameObject.GetComponent<NPC_Talk>().talkStarted;
        if (phase != actualPhase && !talkStarted)
        {
            gameObject.GetComponent<NPC_Talk>().StartConversation(phase);   
            actualPhase = phase;
        }
        talkEnded = !gameObject.GetComponent<NPC_Talk>().talkStarted;

        if (talkEnded && !talkStarted)
        {
            yield return new WaitForSeconds(0.5f);
            gameStates.GetComponent<StatesScript>().state = GameStates.FOETURN;
            actualPhase = -1;
        }
        
    }

    IEnumerator IntroduceLuck()
    {
        bool talkEnded = false;
        int phase = (explainedLuck ? 21 : 20);
        bool talkStarted = gameObject.GetComponent<NPC_Talk>().talkStarted;
        if (phase != actualPhase && !talkStarted)
        {
            gameObject.GetComponent<NPC_Talk>().StartConversation(phase, (explainedLuck ? NPC_Talk.CLICK_TO_YESNO : NPC_Talk.CLICK_TO_CONTINUE));
            actualPhase = phase;
        }
        talkEnded = !gameObject.GetComponent<NPC_Talk>().talkStarted;

        if (talkEnded && !talkStarted)
        {
            explainedLuck = true;
            actualPhase = -1;
            if (phase == 21)
            {
                hasFinishedLuckText = true;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void RollDice()
    {
        if (!foeDamage.GetComponent<TextScript>().enabled)
        {
            foeDamage.GetComponent<TextScript>().enabled = true;
        }
        if (!foeDiceRolled)
        {
            foeDices.GetComponent<DiceScript>().RollDice();
        }
        foeDiceRolled = true;
    }

    private void WriteStats(string strengthTxt, string expertiseTxt, string luckTxt)
    {
        strengthText.text = strengthTxt;
        expertiseText.text = expertiseTxt;
        luckText.text = luckTxt;
    }

    public void TryLuck(bool accepted)
    {
        Debug.Log("AQUI!!!!!!!!!!!!!!!!!");
        if (accepted)
        {
            player.GetComponent<PlayerManagement>().rollLuck = true;
        } else
        {
            gameStates.GetComponent<StatesScript>().state = GameStates.PLAYERTURN;  
        }
    }

    public void exec(string method, object[] parameters)
    {
        GetType().GetMethod(method).Invoke(this, parameters);
    }
}
