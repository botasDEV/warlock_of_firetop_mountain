using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthGuardJackActions : MonoBehaviour, IActions 
{ 
    public void exec(string method, object[] parameters)
    {
        GetType().GetMethod(method).Invoke(this, parameters);
    }

    public void GiveUp(bool end)
    {
        if (end)
        {
            Debug.Log("YOU LOST");
        } else
        {
            Debug.Log("GO ON!");
        }
    }

}
