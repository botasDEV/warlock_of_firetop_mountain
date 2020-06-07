using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Rotate : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 playerPosition = other.transform.position;
            Vector3 npcPosition = transform.position;
            
            Vector3 delta = new Vector3(playerPosition.x - npcPosition.x, 0.0f, playerPosition.z - npcPosition.z);
            Quaternion rotation = Quaternion.LookRotation(delta);
            transform.rotation = rotation;
        }
    }
}
