using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GipsyKaylaActions : MonoBehaviour, IActions
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void exec(string method, object[] parameters)
    {
        GetType().GetMethod(method).Invoke(this, parameters);
    }

    public void DiceOfLight(bool accepted)
    {
        if (accepted)
        {
            player.GetComponent<ManagePlayer>().Dice = 1;
            player.GetComponent<ManagePlayer>().Phase += 1;
        }
    }
}
