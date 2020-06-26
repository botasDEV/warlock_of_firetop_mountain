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
    public Text playerLuck;
    
    int actualPhase = -1;
    bool foeDiceRolled = false;
    public bool explainedLuck = false;
    bool hasFinishedLuckText = false;
    public bool checkLuck = false;

    public int currentStrength;
    public int currentExpertise;



    // Start is called before the first frame update
    void Start()
    {
        npc = gameObject.GetComponent<NPC_Talk>().npc;

        currentStrength = npc.stats.strength;
        currentExpertise = npc.stats.expertise;
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
        if (!strengthText.text.Equals(strengthTxt) || !expertiseText.text.Equals(expertiseTxt))
        {
            WriteStats(strengthTxt, expertiseTxt);
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
        
        if (actualState == GameStates.LUCK && foeDamage.text != "0" && playerDamage.text != "0"  && checkLuck)
        {
            StartCoroutine(CheckLuck());
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

    IEnumerator CheckLuck()
    {
        checkLuck = false;
        yield return new WaitForSeconds(0.5f);

        GameStates nextState = GameStates.PLAYERTURN;
        int foeValue = int.Parse(foeDamage.text);
        int playerValue = int.Parse(playerDamage.text);
        int luck = int.Parse(playerLuck.text.Split('/')[0]);

        int maxFoeStregth = int.Parse(strengthText.text.Split('/')[1]);
        int maxPlayerStregth = int.Parse(player.GetComponent<PlayerManagement>().strengthText.text.Split('/')[1]);
        int gottenLuckPoints = player.GetComponent<PlayerManagement>().gotLuckPoints;

        if (playerValue > foeValue && gottenLuckPoints <= luck)
        {
            currentStrength -= 2;

            if (currentStrength <= 0)
            {
                currentStrength = 0;
                nextState = GameStates.WON;
            }
        } else if (playerValue > foeValue)
        {
            currentStrength += 1;
            if (currentStrength >= maxFoeStregth)
            {
                currentStrength = maxFoeStregth;
            }
        }

        if (playerValue < foeValue && gottenLuckPoints <= luck)
        {
            int strength = player.GetComponent<PlayerManagement>().currentStrength;
            strength += 1;

            if (strength >= maxPlayerStregth)
            {
                strength = maxPlayerStregth;
            }

            player.GetComponent<PlayerManagement>().currentStrength = strength;
        } else if (playerValue < foeValue)
        {
            player.GetComponent<PlayerManagement>().currentStrength -= 1;

            if (player.GetComponent<PlayerManagement>().currentStrength <= 0)
            {
                player.GetComponent<PlayerManagement>().currentStrength = 0;
                nextState = GameStates.LOST;
            }
        }

        player.GetComponent<PlayerManagement>().canReset = true;
        player.GetComponent<PlayerManagement>().gotLuckPoints = 0;
        yield return new WaitForSeconds(0.5f);
        gameStates.GetComponent<StatesScript>().state = nextState;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerManagement>().currentLuck = luck - 1;
        hasFinishedLuckText = false;

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

    private void WriteStats(string strengthTxt, string expertiseTxt)
    {
        strengthText.text = strengthTxt;
        expertiseText.text = expertiseTxt;
    }

    public void TryLuck(bool accepted)
    {
        StartCoroutine(DecidedLuck(accepted));
    }

    IEnumerator DecidedLuck(bool accepted)
    {
        if (accepted)
        {
            player.GetComponent<PlayerManagement>().rollLuck = true;
            
        }
        else
        {
            player.GetComponent<PlayerManagement>().canReset = true;
            yield return new WaitForSeconds(0.5f);
            gameStates.GetComponent<StatesScript>().state = GameStates.PLAYERTURN;
            yield return new WaitForSeconds(0.5f);
            hasFinishedLuckText = false;
        }

    }

    public void exec(string method, object[] parameters)
    {
        GetType().GetMethod(method).Invoke(this, parameters);
    }
}
