using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string questName;
    public int[] npcIds;

    public QuestData(string name, int[] npcIds)
    {
        this.questName = name;
        this.npcIds  = npcIds;
    }
}
