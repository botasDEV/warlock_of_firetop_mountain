using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    private bool isGrounded = false;
    private bool gotValue = false;
    public bool thrown = false;
    private GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = gameObject.transform.parent.gameObject;
    }

 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
            gotValue = false;
            thrown = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" && !isGrounded && !gotValue && thrown)
        {
            gotValue = true;
            isGrounded = true;
            thrown = false;

            int value = 7 - int.Parse(gameObject.name);
            if (parentGameObject.name == "Dice0")
            {
                parentGameObject.transform.parent.GetComponent<DiceManager>().dice0Value = 7 - int.Parse(gameObject.name);

            }
            else if(parentGameObject.name == "Dice1")
            {
                parentGameObject.transform.parent.GetComponent<DiceManager>().dice1Value = 7 - int.Parse(gameObject.name);
            }
        }
    }
}
