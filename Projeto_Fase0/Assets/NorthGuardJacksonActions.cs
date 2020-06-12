using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthGuardJacksonActions : MonoBehaviour, IActions
{
    private GameObject player;

    public void exec(string method, object[] parameters)
    {
        GetType().GetMethod(method).Invoke(this, parameters);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void StartAdventure(bool accepted)
    {
        if (accepted)
        {
            Debug.Log("StartAdventure");
        }
    }


}
