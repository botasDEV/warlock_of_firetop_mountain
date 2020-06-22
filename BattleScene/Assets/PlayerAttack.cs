using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject foeTarget;
    public GameObject foe;
    public GameObject gameStats;
    public float speed = 15f;
    Vector3 initialPosition;
    Vector3 foePosition;
    bool attacked = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        foePosition = foeTarget.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStats.GetComponent<StatesScript>().state == GameStates.ATTACK && !gameObject.GetComponent<Renderer>().enabled)
        {
            Debug.Log("AQUI");
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        if (gameStats.GetComponent<StatesScript>().state == GameStates.ATTACK && gameObject.GetComponent<Renderer>().enabled)
        {
            Debug.Log("AQUI 2");
            transform.position = Vector3.MoveTowards(transform.position, foePosition, Time.deltaTime * speed);
        }

        if (gameStats.GetComponent<StatesScript>().state == GameStates.ATTACK && transform.position == foePosition)
        {
            Debug.Log("AQUI 3");
            gameObject.GetComponent<Renderer>().enabled = false;
            transform.position = initialPosition;
            int currentStrength = foe.GetComponent<NPC_Manage>().currentStrength - 2;
            if (currentStrength <= 0)
            {
                currentStrength = 0;
                gameStats.GetComponent<StatesScript>().state = GameStates.WON;
            }
            else
            {
                gameStats.GetComponent<StatesScript>().state = GameStates.LUCK;
            }
            foe.GetComponent<NPC_Manage>().currentStrength = currentStrength;
        }
    }

    
}
