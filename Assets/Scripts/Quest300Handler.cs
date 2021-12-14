using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest300Handler:QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context) {
        if(actionName == QuestHandler.EVENT_END_OF_TALK){
            if(GetQuestStep(context) == 0) {
                SceneManager.LoadScene(TOWNSTAGE2);
            }
        }
    }
}