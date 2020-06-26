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

    public Text expertisePotionQtd;
    public Text luckPotionQtd;
    public Text strengthPotionQtd;

    public bool rollLuck = false;
    public bool canReset = false;
    bool isPlaying = false;
    

    private int strength = 10;
    private int expertise = 10;
    private int luck = 10;
    public int playerDamage = 0;
    public Text foeDamage;

    public int currentStrength;
    public int currentExpertise;
    public int currentLuck;
    public int gotLuckPoints = 0;


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

        if (actualState == GameStates.PLAYERTURN && (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && !expertisePotionQtd.text.Equals("0"))
        {
            int maxExpertisePoints = int.Parse(expertiseText.text.Split('/')[1]);
            if (currentExpertise < maxExpertisePoints)
            {
                currentExpertise = maxExpertisePoints;
                int qtd = int.Parse(expertisePotionQtd.text) - 1;
                if (qtd <= 0)
                {
                    qtd = 0;
                }
                expertisePotionQtd.text = qtd.ToString();
            }
        }

        if (actualState == GameStates.PLAYERTURN && (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && !strengthPotionQtd.text.Equals("0"))
        {
            int maxStrengthPoints = int.Parse(strengthText.text.Split('/')[1]);
            if (currentStrength < maxStrengthPoints)
            {
                currentStrength = maxStrengthPoints;
                int qtd = int.Parse(strengthPotionQtd.text) - 1;
                if (qtd <= 0)
                {
                    qtd = 0;
                }
                strengthPotionQtd.text = qtd.ToString();
            }
        }

        if (actualState == GameStates.PLAYERTURN && (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && !luckPotionQtd.text.Equals("0"))
        {
            int maxLuckPoints = int.Parse(luckText.text.Split('/')[1]);
            if (currentLuck < maxLuckPoints)
            {
                currentLuck = maxLuckPoints;
                int qtd = int.Parse(luckPotionQtd.text) - 1;
                if (qtd <= 0)
                {
                    qtd = 0;
                }
                luckPotionQtd.text = qtd.ToString();
            }
        }

        if (actualState == GameStates.PLAYERMAINPHASE && Input.GetKeyDown(KeyCode.E) && damageText.text != "0" || canReset)
        {
            dicesChecker.GetComponent<CheckDiceFace>().ResetCheck();
            playerDices.GetComponent<DiceScript>().ResetDice();
            isPlaying = false;
            canReset = false;
        }

        if (actualState == GameStates.LUCK && rollLuck && Input.GetMouseButtonDown(0))
        {
            StartLuck();
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

    void StartLuck()
    {
        playerDices.GetComponent<DiceScript>().RollDice();
    }
}
