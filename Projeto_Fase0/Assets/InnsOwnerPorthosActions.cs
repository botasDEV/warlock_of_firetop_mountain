using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnsOwnerPorthosActions : MonoBehaviour, IActions
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

    public void AddTenCoins(bool accepted)
    {
        if (accepted && player.GetComponent<ManagePlayer>().Money < 100)
        {
            int money = player.GetComponent<ManagePlayer>().Money + 10;
            if (money > 100) money = 100;
            player.GetComponent<ManagePlayer>().Money = money;
        }
    }

    public void AddFiveCoins(bool accepted)
    {
        if (accepted && player.GetComponent<ManagePlayer>().Money < 100)
        {
            int money = player.GetComponent<ManagePlayer>().Money + 5;
            if (money > 100) money = 100;
            player.GetComponent<ManagePlayer>().Money = money;
        }
    }
}
