using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    private int questIndex;

    private int npcId;
    private string questName;

    private int questStatus;

    public QuestData(int questIndex, int npcId, string questName, int questStatus)
    {
        this.questIndex = questIndex;
        this.npcId = npcId;
        this.questName = questName;
        this.questStatus = questStatus;
    }

    public int GetQuestIndex() {
        return questIndex;
    }

    public int GetNpcId() {
        return npcId;
    }

    public string GetQuestName() {
        return questName;
    }

    public virtual int GetQuestStatus() {
        return questStatus;
    }

    public virtual void AddQuestStatus() {
        this.questStatus += 10;
    }

    public virtual void ResetQuestStatus() {
        this.questStatus = 0;
    }

    public virtual bool isDoingQuest() {
        return false;
    }

    public virtual void NotifyEvent(Object eventData){
    }

    public virtual void Update(){
    }
}
