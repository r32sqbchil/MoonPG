using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleStageQuest8Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            if(GetQuestStep(context) == 0)
            {
                CastleStageSetting setting = GameObject.FindObjectOfType<CastleStageSetting>();
                if(setting != null){
                    setting.ActivatePortal(GetPortalObject(), context);
                    setting.HideWall();
                    
                } else {
                    Debug.LogWarning("Can't find a component - CastleStageSetting");
                }
                SetQuestStep(context, 10);
            }
        }
    }

    Portal GetPortalObject(){
        foreach(Portal portal in GameObject.FindObjectsOfType<Portal>(true)){
            return portal;
        }
        return null;
    }
}
