using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, PLAYERMAINPHASE, FOETURN, MAINPHASE, WON, LOST}

public class StatesScript : MonoBehaviour
{
    public GameStates state;
    private bool hasWon = false;
    private bool hasLost = false;

    // Start is called before the first frame update
    void Start()
    {
        state = GameStates.START;
    }

    public void NextState()
    {
        switch(state)
        {
            case GameStates.START:
                state = GameStates.PLAYERTURN;
                break;
            case GameStates.PLAYERTURN:
                state = GameStates.PLAYERMAINPHASE;
                break;
            case GameStates.PLAYERMAINPHASE:
                state = GameStates.FOETURN;
                break;
            case GameStates.FOETURN:
                state = GameStates.MAINPHASE;
                break;
            case GameStates.MAINPHASE:
                if (hasWon)
                {
                    state = GameStates.WON;
                } else if(hasLost)
                {
                    state = GameStates.LOST;
                } else
                {
                    state = GameStates.PLAYERTURN;
                }
                
                break;

        }
    }

    
}
