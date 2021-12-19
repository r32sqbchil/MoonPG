using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainStage6Quest5Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            if(GetQuestStep(context) == 0)
            {
                MountainStage6Setting mountainStage6Setting = GameObject.FindObjectOfType<MountainStage6Setting>();
                if(mountainStage6Setting != null){
                    mountainStage6Setting.HideWall();
                } else {
                    Debug.LogWarning("Can't find a component - MountainStage6Setting");
                }
                SetQuestStep(context, 10);
            }
        }
    }
}
