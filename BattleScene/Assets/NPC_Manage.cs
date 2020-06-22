using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manage : MonoBehaviour
{
    private Npc npc;

    public GameObject gameStates;
    public GameObject foeDices;
    public Text playerDamage;
    public Text foeDamage;
    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    int actualPhase = -1;
    bool foeDiceRolled = false;

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
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;

        if (actualState == GameStates.FOETURN)
        {
            RollDice();
        }

        if (actualState == GameStates.PLAYERMAINPHASE)
        {
            StartCoroutine(ReactToPlayer());
        }

        string strengthTxt = currentStrength + " / " + npc.stats.strength;
        string expertiseTxt = currentExpertise + " / " + npc.stats.expertise;
        string luckTxt = currentLuck + " / " + npc.stats.luck;
        if (!strengthText.text.Equals(strengthTxt) || !expertiseText.text.Equals(expertiseTxt) || !luckText.text.Equals(luckTxt))
        {
            WriteStats(strengthTxt, expertiseTxt, luckTxt);
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

    
}
