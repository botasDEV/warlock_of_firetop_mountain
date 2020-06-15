using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    private bool isGrounded = true;
    private bool gotValue = false;
    private GameObject parentGameObject;
    private Rigidbody parentRigidBody;

    private void Start()
    {
        parentGameObject = gameObject.transform.parent.gameObject;
        parentRigidBody = parentGameObject.GetComponent<Rigidbody>();
    }

 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
            gotValue = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" && parentRigidBody.IsSleeping() && !isGrounded && !gotValue)
        {
            gotValue = true;
            isGrounded = true;

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
