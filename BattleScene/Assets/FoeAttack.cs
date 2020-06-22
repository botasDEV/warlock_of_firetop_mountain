using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeAttack : MonoBehaviour
{
    public GameObject playerTarget;
    public GameObject player;
    public GameObject gameStats;
    public float speed = 15f;
    Vector3 initialPosition;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        playerPosition = playerTarget.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND && !gameObject.GetComponent<Renderer>().enabled)
        {
            Debug.Log("AQUI");
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND && gameObject.GetComponent<Renderer>().enabled)
        {
            Debug.Log("AQUI 2");
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * speed);
        }
        if (gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND && transform.position == playerPosition)
        {
            Debug.Log("AQUI 3");
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

    IEnumerator Defend()
    {
        while (gameStats.GetComponent<StatesScript>().state == GameStates.DEFEND)
        {
            

            

            yield return null;
        }
    }

    
}
