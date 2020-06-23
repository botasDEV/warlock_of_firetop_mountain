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

    bool isPlaying = false;

    private int strength = 1;
    private int expertise = 1;
    private int luck = 1;
    public int playerDamage = 0;
    public Text foeDamage;

    public int currentStrength;
    public int currentExpertise;
    public int currentLuck;


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
        GameStates actualState = gameStates.GetComponent<StatesScript>().state;

        if (actualState == GameStates.PLAYERTURN && Input.GetMouseButtonDown(0) && !isPlaying)
        {
            if (!damageText.GetComponent<TextScript>().enabled)
            {
                damageText.GetComponent<TextScript>().enabled = true;
            }
            playerDices.GetComponent<DiceScript>().RollDice();
            isPlaying = true;
        }

        if (actualState == GameStates.PLAYERMAINPHASE && Input.GetKeyDown(KeyCode.E) && damageText.text != "0")
        {
            dicesChecker.GetComponent<CheckDiceFace>().ResetCheck();
            playerDices.GetComponent<DiceScript>().ResetDice();
            isPlaying = false;
        }

        if (actualState == GameStates.LUCK && Input.GetMouseButtonDown(0))
        {
            Debug.Log("LUCK STATE");
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
