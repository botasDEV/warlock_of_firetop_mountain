using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Manage : MonoBehaviour
{
    public Npc npc;
    public Text strengthText;
    public Text expertiseText;
    public Text luckText;

    // Start is called before the first frame update
    void Start()
    {
        npc = GameObject.FindGameObjectWithTag("Foe").GetComponent<NPC_Talk>().npc;
        strengthText.text = npc.stats.strength.ToString();
        expertiseText.text = npc.stats.expertise.ToString() + " / " + npc.stats.expertise.ToString();
        luckText.text = npc.stats.luck.ToString() + " / " + npc.stats.luck.ToString();
    }

    
}
