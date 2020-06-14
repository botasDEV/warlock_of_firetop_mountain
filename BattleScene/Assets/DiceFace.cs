using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    private bool isGrounded = false;
    private bool gotValue = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        gotValue = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isGrounded && !gotValue)
        {
            Debug.Log(7 - int.Parse(gameObject.name));
            gotValue = true;

        }
    }
}
