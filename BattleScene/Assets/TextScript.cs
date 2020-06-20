using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    Text damageText;
    private int dicesNumber = 0;
    int dicesCounted = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dicesCounted == 2)
        {
            damageText.text = dicesNumber.ToString();
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
