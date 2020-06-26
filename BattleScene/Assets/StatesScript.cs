using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates { START, PLAYERTURN, PLAYERMAINPHASE, FOETURN, MAINPHASE, LUCK, ATTACK, DEFEND, ATTACKDEFEND, WON, LOST}

public class StatesScript : MonoBehaviour
{
    public GameStates state;
    public GameStates lastState = GameStates.START;
    public Text label;
    public Text labelShadow;

    Dictionary<GameStates, string> stateLabels = new Dictionary<GameStates, string>();

    // Start is called before the first frame update
    void Start()
    {
        state = GameStates.PLAYERTURN;
        stateLabels.Add(GameStates.PLAYERTURN, "Player Turn");
        stateLabels.Add(GameStates.FOETURN, "Foe Turn");
        stateLabels.Add(GameStates.LUCK, "Luck");
        stateLabels.Add(GameStates.WON, "You have won!");
        stateLabels.Add(GameStates.LOST, "That a shame.. you lost!");
    }

    private void Update()
    {
        if (state != lastState && stateLabels.ContainsKey(state))
        {
            lastState = state;
            label.text = stateLabels[state];
            labelShadow.text = stateLabels[state];
        }
    }

}
