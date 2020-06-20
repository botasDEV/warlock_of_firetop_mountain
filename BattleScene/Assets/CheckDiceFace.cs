using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckDiceFace : MonoBehaviour
{
    List<Vector3> diceVelocities;
    public Text playerDamage;
    public Text foeDamage;
    public bool isPlaying = false;
    int diceExit = 0;
    int diceCounted = 0;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        diceVelocities = DiceScript.velocities;
    }

    private void OnTriggerExit(Collider other)
    {
        diceExit++;
        if (diceExit >= 2) {
            isPlaying = true;
            diceCounted = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (diceVelocities.Count == 2 && isPlaying)
        {
            float velocityValue = 0f;
            foreach (Vector3 diceVelocity in diceVelocities){
                velocityValue += (diceVelocity.x + diceVelocity.y + diceVelocity.z);
            }

            if (velocityValue == 0f)
            {
                int value = 7 - int.Parse(other.gameObject.tag);
                GameObject dice = other.transform.parent.gameObject;
                if (dice.tag.Equals("Player"))
                {
                    playerDamage.GetComponent<TextScript>().SumDices(value);
                }
                else
                {
                    foeDamage.GetComponent<TextScript>().SumDices(value);
                }

                diceCounted++;
                if (diceCounted == 2)
                {
                    isPlaying = false;
                }
            }

        }
    }

    public void ResetCheck()
    {
        isPlaying = false;
        diceExit = 0;
    }
}
