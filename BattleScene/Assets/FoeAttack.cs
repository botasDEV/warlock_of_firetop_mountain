using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeAttack : MonoBehaviour
{
    public GameObject playerTarget;
    public GameObject player;
    public GameObject gameStats;
    public float speed = 15f;
    public Vector3 initialPosition;
    Vector3 playerPosition;
    public bool attack = true; 

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerTarget.transform.position;
        if ((gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND || gameStats.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND) && !gameObject.GetComponent<Renderer>().enabled && attack)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        if ((gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND || gameStats.GetComponent<StatesScript>().state == GameStates.ATTACKDEFEND) && gameObject.GetComponent<Renderer>().enabled && attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        }

        if (gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND && Vector3.Distance(transform.position, playerPosition) < 0.5)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            transform.position = initialPosition;
            int currentStrength = player.GetComponent<PlayerManagement>().currentStrength - 2;
            if (currentStrength <= 0)
            {
                currentStrength = 0;
                gameStats.GetComponent<StatesScript>().state = GameStates.LOST;
            }
            else
            {
                gameStats.GetComponent<StatesScript>().state = GameStates.LUCK;
            }
            player.GetComponent<PlayerManagement>().currentStrength = currentStrength;
        }

    }

   

    
}
