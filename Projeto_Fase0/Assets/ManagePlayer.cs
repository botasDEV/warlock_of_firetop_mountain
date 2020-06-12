using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayer : MonoBehaviour
{
    public Text strengthText;
    public Text expertiseText;
    public Text luckText;
    public Text moneyText;
    public Text foodText;
    public Text expertisePotionText;
    public Text strengthPotionText;
    public Text luckPotionText;
    public Text diceText;

    private int _phase = 0;
    private int _strength = 0;
    private int _dice = 0;
    private int _expertise = 0;
    private int _luck = 0;
    private int _money = 0;
    private int _food = 0;
    private int _expertisePotion = 0;
    private int _strengthPotion = 0;
    private int _luckPotion = 0;

    public int Phase { get { return _phase; } set { _phase = value; OnChanged(); } }
    public int Strength { get { return _strength; } set { _strength = value; OnChanged(); } }
    public int Expertise { get { return _expertise; } set { _expertise = value; OnChanged(); } }
    public int Luck { get { return _luck; } set { _luck = value; OnChanged(); } }
    public int Money { get { return _money; } set { _money = value; OnChanged(); } }
    public int Food { get { return _food; } set { _food = value; OnChanged(); } }
    public int ExpertisePotion { get { return _expertisePotion; } set { _expertisePotion = value; OnChanged(); } }
    public int StrengthPotion { get { return _strengthPotion; } set { _strengthPotion = value; OnChanged(); } }
    public int LuckPotion { get { return _luckPotion; } set { _luckPotion = value; OnChanged(); } }
    public int Dice { get { return _dice; } set { _dice = value; OnChanged(); } }
    private bool hasChanges = false;


    private void Start()
    {
        strengthText.text = Strength.ToString();
        expertiseText.text = Expertise.ToString();
        luckText.text = Luck.ToString();
        moneyText.text = Money.ToString() + "/100";
        foodText.text = Food.ToString();
        expertisePotionText.text = ExpertisePotion.ToString() + "/1";
        strengthPotionText.text = StrengthPotion.ToString() + "/1";
        luckPotionText.text = LuckPotion.ToString() + "/1";
        diceText.text = Dice.ToString();
    }

    private void Update()
    {
        if (hasChanges)
        {
            strengthText.text = Strength.ToString();
            expertiseText.text = Expertise.ToString();
            luckText.text = Luck.ToString();
            moneyText.text = Money.ToString() + "/100";
            foodText.text = Food.ToString();
            expertisePotionText.text = ExpertisePotion.ToString() + "/1";
            strengthPotionText.text = StrengthPotion.ToString() + "/1";
            luckPotionText.text = LuckPotion.ToString() + "/1";
            diceText.text = Dice.ToString();
            hasChanges = false;
        }
    }

    private void OnChanged() { hasChanges = true; }

}
