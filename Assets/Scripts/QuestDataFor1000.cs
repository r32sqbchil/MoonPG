using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDataFor1000 : QuestData
{
    private int huntCount;
    private int missionCount;
    // public Text missionCountText;


    public QuestDataFor1000(int questIndex, int npcId, string questName, int questStatus) : base(questIndex, npcId, questName, questStatus)
    {
        this.huntCount = 0;
        this.missionCount = 3;
    }

    public override bool isDoingQuest() {
        int questStauts = GetQuestStatus();
        return questStauts == 20 || questStauts == 40;
    }

    public override void ResetQuestStatus() {
        Debug.Log("hunt: " + huntCount);
        base.ResetQuestStatus();
        this.huntCount = 0;
    }

    public override void NotifyEvent(Object eventData){
        if(isDoingQuest() && eventData is Enemy){
            huntCount++;
            if(missionCount <= huntCount){
                GameObject.FindObjectOfType<QuestManager>().QuestComplete(GetNpcId(), false);
            }
        }
    }
    public override void Update(){
        // missionCountText.text = this.huntCount + "  /  " + this.missionCount;
    }
}