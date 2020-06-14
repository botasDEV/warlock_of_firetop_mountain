using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonParser
{
    private TextAsset jsonFile;
    private Npcs npcs;
    private string npcName;

    public JsonParser(TextAsset jsonFile, string npcName)
    {
        this.jsonFile = jsonFile;
        this.npcName = npcName;
    }

    public Npc Setup()
    {
        this.npcs = JsonUtility.FromJson<Npcs>(jsonFile.text); 
        foreach(Npc npc in this.npcs.npcs)
        {
            if (this.npcName.Equals(npc.name))
            {
                return npc;
            }
        }

        return null;
    }
    
}
