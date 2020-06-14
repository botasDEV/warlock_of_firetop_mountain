using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{

    string gameObjectName;
    public Vector3 impulse = new Vector3(5.0f, 0.0f, 0.0f);
    GameObject dice0, dice1;
    public int dice0Value, dice1Value;


    private void Start()
    {
        gameObjectName = gameObject.name;
        dice0 = gameObject.transform.GetChild(0).gameObject;
        dice1 = gameObject.transform.GetChild(1).gameObject;
    }

    public void RollDice()
    {
        Rigidbody dice0Rb = dice0.GetComponent<Rigidbody>();
        Rigidbody dice1Rb = dice1.GetComponent<Rigidbody>();
        
        dice0Rb.AddForce(impulse, ForceMode.Impulse);
        dice1Rb.AddForce(impulse, ForceMode.Impulse);

        dice0Rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        dice1Rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }
}
