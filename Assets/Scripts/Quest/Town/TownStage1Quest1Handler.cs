using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStage1Quest1Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            if(GetQuestStep(context) == 0)
            {
                TownStage1Setting townStage1Setting = GameObject.FindObjectOfType<TownStage1Setting>();
                if(townStage1Setting != null){
                    townStage1Setting.HideWall();
                } else {
                    Debug.LogWarning("Can't find a component - TownStage1Setting");
                }
                SetQuestStep(context, 10);
            }
        }
    }
}
