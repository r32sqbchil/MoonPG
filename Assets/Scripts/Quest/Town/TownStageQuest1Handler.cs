using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownStageQuest1Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            SceneManager.LoadScene(TOWNSTAGE1);
        }
    }
}

