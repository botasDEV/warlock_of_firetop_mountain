using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject foeTarget;
    public GameObject foe;
    public GameObject foeDamage;
    public GameObject gameStates;
    public GameObject checkDice;
    public float speed = 15f;
    Vector3 initialPosition;
    Vector3 foePosition;
    int actualPhase = -1;
    bool attack = true;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       

        foePosition = foeTarget.transform.position;
        if ((gameStates.GetComponent<StatesScript>().state == GameStates.ATTACK || gameStates.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND) && !gameObject.GetComponent<Renderer>().enabled && attack)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        if ((gameStates.GetComponent<StatesScript>().state == GameStates.ATTACK || gameStates.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND) && gameObject.GetComponent<Renderer>().enabled && attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, foePosition, Time.deltaTime * speed);
        }

        if (gameStates.GetComponent<StatesScript>().state == GameStates.ATTACK && Vector3.Distance(transform.position, foePosition) < 0.5)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            transform.position = initialPosition;
            int currentStrength = foe.GetComponent<NPC_Manage>().currentStrength - 2;
            if (currentStrength <= 0)
            {
                currentStrength = 0;
                gameStates.GetComponent<StatesScript>().state = GameStates.WON;
            }
            else
            {
                gameStates.GetComponent<StatesScript>().state = GameStates.LUCK;
            }
            foe.GetComponent<NPC_Manage>().currentStrength = currentStrength;
        }

        if (gameStates.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND && Vector3.Distance(transform.position, foePosition) < 0.5)
        {
            attack = false;
            foeTarget.GetComponent<FoeAttack>().attack = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            foeTarget.GetComponent<Renderer>().enabled = false;

            gameObject.transform.position = initialPosition;
            foeTarget.transform.position = foeTarget.GetComponent<FoeAttack>().initialPosition;
            
            int phase = 12;
            if (actualPhase != phase)
            {
                foe.GetComponent<NPC_Talk>().StartConversation(phase);
                actualPhase = phase;
            }
        }

        if (gameStates.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND && !attack && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ResetAfterDraw());
        }
    }
 
    
    IEnumerator ResetAfterDraw()
    {
        checkDice.GetComponent<CheckDiceFace>().ResetCheck();
        yield return new WaitForSeconds(0.5f);
        gameStates.GetComponent<StatesScript>().state = GameStates.PLAYERTURN;
        yield return new WaitForSeconds(0.5f);
        attack = true;
        foeTarget.GetComponent<FoeAttack>().attack = true;
    }
}
