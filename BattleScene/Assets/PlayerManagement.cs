using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{
    public GameObject gameStates;
    public GameObject playerDices;
    public GameObject dicesChecker;

    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    public Text damageText;
    

    private int strength = 1;
    private int expertise = 1;
    private int luck = 1;
    public static int damage = 0;

    private int currentStrength;
    private int currentExpertise;
    private int currentLuck;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentStrength = strength;
        currentExpertise = expertise;
        currentLuck = luck;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameStates.GetComponent<StatesScript>().state == GameStates.PLAYERTURN && Input.GetMouseButtonDown(0))
        {
            if (!damageText.GetComponent<TextScript>().enabled)
            {
                damageText.GetComponent<TextScript>().enabled = true;
            }
            playerDices.GetComponent<DiceScript>().RollDice();
        }

        if (gameStates.GetComponent<StatesScript>().state == GameStates.PLAYERTURN && Input.GetMouseButtonDown(0) && damageText.text != "0")
        {
            dicesChecker.GetComponent<CheckDiceFace>().ResetCheck();
            damageText.GetComponent<TextScript>().ResetText();
            damageText.GetComponent<TextScript>().enabled = false;
            playerDices.GetComponent<DiceScript>().ResetDice();
        }

        string strengthTxt = currentStrength + " / " + strength;
        string expertiseTxt = currentExpertise + " / " + expertise;
        string luckTxt = currentLuck + " / " + luck;
        if (!strengthText.text.Equals(strengthTxt) || !expertiseText.text.Equals(expertiseTxt) || !luckText.text.Equals(luckTxt))
        {
            WriteStats(strengthTxt, expertiseTxt, luckTxt);
        }
    }

    private void WriteStats(string strengthTxt, string expertiseTxt, string luckTxt)
    {
        strengthText.text = strengthTxt;
        expertiseText.text = expertiseTxt;
        luckText.text = luckTxt;
    }
}
