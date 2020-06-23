using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public GameObject gameStates;
    Text damageText;
    private int dicesNumber = 0;
    int dicesCounted = 0;
    bool reseted = false;

    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;

        if (dicesCounted == 2 && damageText.text == "0")
        {
            damageText.text = dicesNumber.ToString();            
        } 
        
        if (actualState == GameStates.LUCK || actualState == GameStates.PLAYERTURN && !reseted)
        {
            ResetText();
        }
    }

    public void SumDices(int faceValue)
    {
        if (dicesCounted < 2)
        {
            dicesNumber += faceValue;
            dicesCounted += 1;
        }
    }

    public void ResetText()
    {
        dicesNumber = 0;
        dicesCounted = 0;
        damageText.text = "0";
    }
}
