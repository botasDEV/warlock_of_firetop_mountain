using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{
    public bool canPlay;
    GameObject playerDices;
    GameObject foeDices;
    Vector3 playerDicesInitialPosition;

    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    public Text damageText;

    private int strength = 1;
    private int expertise = 1;
    private int luck = 1;
    private int currentStrength;
    private int currentExpertise;
    private int currentLuck;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canPlay = false;
        playerDices = GameObject.Find("Dice_Player");
        foeDices = GameObject.Find("Dice_Foe");
        currentStrength = strength;
        currentExpertise = expertise;
        currentLuck = luck;
        playerDicesInitialPosition = playerDices.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay && Input.GetMouseButtonDown(0))
        {
            canPlay = false; 
            playerDices.GetComponent<DiceManager>().RollDice();
        }

        // Sets the Damage after rolling dice
        if (playerDices.GetComponent<DiceManager>().dice0Value > 0 && playerDices.GetComponent<DiceManager>().dice1Value > 0)
        {
            damageText.text = (playerDices.GetComponent<DiceManager>().dice0Value + playerDices.GetComponent<DiceManager>().dice1Value + expertise).ToString();
            playerDices.GetComponent<DiceManager>().dice0Value = 0;
            playerDices.GetComponent<DiceManager>().dice1Value = 0;
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
