using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manage : MonoBehaviour
{
    private Npc npc;

    public GameObject gameStates;
    public Text playerDamage;
    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    int actualPhase = -1;

    // Start is called before the first frame update
    void Start()
    {
        npc = gameObject.GetComponent<NPC_Talk>().npc;
        strengthText.text = npc.stats.strength.ToString();
        expertiseText.text = npc.stats.expertise.ToString() + " / " + npc.stats.expertise.ToString();
        luckText.text = npc.stats.luck.ToString() + " / " + npc.stats.luck.ToString();
    }

    private void Update()
    {
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;
        if (actualState == GameStates.PLAYERMAINPHASE)
        {
            Debug.Log("REACTING");
            ReactToPlayer();
        }

        if (actualState == GameStates.FOETURN)
        {

            RollDice();
        }
    }

    void ReactToPlayer()
    {
        bool talkEnded = false;
        int phase = (int.Parse(playerDamage.text) > 6 ? 10 : 11);
        bool talkStarted = gameObject.GetComponent<NPC_Talk>().talkStarted;
        Debug.Log(talkStarted);
        if (phase != actualPhase && !talkStarted)
        {
            Debug.Log("GOT HERE!!!!!!!!!!!!!!!!!");
            gameObject.GetComponent<NPC_Talk>().StartConversation(phase);
            
            actualPhase = phase;
          //  yield return new WaitForSeconds(2f);
        }
        talkEnded = !gameObject.GetComponent<NPC_Talk>().talkStarted;

        if (talkEnded)
        {
            Debug.Log("END TALK");
            gameStates.GetComponent<StatesScript>().state = GameStates.FOETURN;
        }
        //yield return new WaitForSeconds(2f);
    }


    void RollDice()
    {
        Debug.Log("ROLLING NPC DICE");
        //yield return new WaitForSeconds(1f);
    }


}
