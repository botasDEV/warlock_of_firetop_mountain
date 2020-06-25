using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDiceFace : MonoBehaviour
{
    List<Vector3> diceVelocities;
    public GameObject playerDices;
    public GameObject foeDices;
    public GameObject gameStates;
    public GameObject foe;
    public GameObject player;
    public Text playerDamage;
    public Text foeDamage;
    public bool isPlayerPlaying = false;
    public bool isFoePlaying = false;
    int dicePlayerExit = 0;
    int diceFoeExit = 0;
    int foeDiceCounter = 0;
    int playerDiceCounter = 0;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;
        if (actualState == GameStates.PLAYERTURN || actualState == GameStates.LUCK)
        {
            diceVelocities = playerDices.GetComponent<DiceScript>().velocities;
        } else if (actualState == GameStates.FOETURN)
        {
            diceVelocities = foeDices.GetComponent<DiceScript>().velocities;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;
        if (actualState == GameStates.PLAYERTURN || actualState == GameStates.LUCK)
        {
            dicePlayerExit++;
            if (dicePlayerExit >= 2)
            {
                isPlayerPlaying = true;
                playerDiceCounter = 0;
            }
        } else if (actualState == GameStates.FOETURN && !isFoePlaying)
        {
            diceFoeExit++;
            if (diceFoeExit >= 2)
            {
                isFoePlaying = true;
                foeDiceCounter = 0;
            }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;
        
        if (diceVelocities !=  null && diceVelocities.Count == 2 && (isPlayerPlaying || isFoePlaying))
        {
            
            float velocityValue = 0f;
            foreach (Vector3 diceVelocity in diceVelocities){
                velocityValue += (diceVelocity.x + diceVelocity.y + diceVelocity.z);
            }
            
            if (velocityValue == 0f)
            {
                
                GameObject dice = other.transform.parent.gameObject;
                int value = 7 - int.Parse(other.gameObject.tag);
                
                if (actualState == GameStates.PLAYERTURN && dice.tag.Equals("Player") && isPlayerPlaying)
                {
                    
                    playerDamage.GetComponent<TextScript>().SumDices(value);
                    playerDiceCounter++;

                    if (playerDiceCounter == 2)
                    {
                        isPlayerPlaying = false;
                        diceVelocities = null;
                        gameStates.GetComponent<StatesScript>().state = GameStates.PLAYERMAINPHASE;
                    }
                }


                if (actualState == GameStates.FOETURN && dice.tag.Equals("Foe") && isFoePlaying)
                {
                    foeDamage.GetComponent<TextScript>().SumDices(value);
                    foeDiceCounter++;
                    if (foeDiceCounter == 2)
                    {
                        isFoePlaying = false;
                        diceVelocities = null;
                    }
                }
                
                if (actualState == GameStates.LUCK && dice.tag.Equals("Player") && isPlayerPlaying)
                {
                    player.GetComponent<PlayerManagement>().gotLuckPoints += value;
                    foeDiceCounter++;
                    if (foeDiceCounter == 2)
                    {
                        isPlayerPlaying = false;
                        diceVelocities = null;
                        foe.GetComponent<NPC_Manage>().checkLuck = true;
                    }
                }

            }

        }
    }

    public void ResetCheck()
    {
        isPlayerPlaying = false;
        isFoePlaying = false;
        diceVelocities = null;
        dicePlayerExit = 0;
        diceFoeExit = 0;
    }
}
