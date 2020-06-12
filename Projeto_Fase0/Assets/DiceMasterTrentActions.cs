using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMasterTrentActions : MonoBehaviour, IActions
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

    public void TrainDice(bool accepted)
    {
        if (accepted)
        {
            Debug.Log("TrainDice");
        }
    }
}
