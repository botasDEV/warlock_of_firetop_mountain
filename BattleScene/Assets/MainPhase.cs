using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPhase : MonoBehaviour
{
    Text playerDamageText;
    Text foeDamageText;
    public GameObject gameStates;

    int foeDamage;
    int playerDamage;
    

    private void Update()
    {
        playerDamageText = GameObject.Find("Damage").GetComponent<Text>();
        foeDamageText = GameObject.Find("Damage_Foe").GetComponent<Text>();
        GameStates currentState = gameStates.GetComponent<StatesScript>().state;
        if (currentState == GameStates.MAINPHASE)
        {
            playerDamage = int.Parse(playerDamageText.text);
            foeDamage = int.Parse(foeDamageText.text);
            Debug.Log("PLAYER: " + playerDamage);
            Debug.Log("FOE: " + foeDamage);
            GameStates nextState;

            if (playerDamage > foeDamage)
            {
                nextState = GameStates.ATTACK;
            } else if (playerDamage < foeDamage)
            {
                nextState = GameStates.DEFEND;
            } else
            {
                nextState = GameStates.ATTACKDEFEND;
            }

            gameStates.GetComponent<StatesScript>().state = nextState;
        }
    }
}
