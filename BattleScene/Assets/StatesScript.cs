using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { START, PLAYERTURN, PLAYERMAINPHASE, FOETURN, MAINPHASE, LUCK, ATTACK, DEFEND, ATTACKDEFEND, WON, LOST}

public class StatesScript : MonoBehaviour
{
    public GameStates state;
  
    // Start is called before the first frame update
    void Start()
    {
        state = GameStates.LUCK;
    }

    private void Update()
    {
        //Debug.Log(state);
    }

}
