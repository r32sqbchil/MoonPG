using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest100Handler:QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context) {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            string sceneName = (string)context[QuestHandler.KEY_OF_SCENE_NAME];
            switch(sceneName)
            {
                case TOWNSTAGE:
                    SceneManager.LoadScene(TOWNSTAGE1);
                    break;
                case TOWNSTAGE1:
                    if(GetQuestStep(context) == 0) {
                        TownStage1Setting townStage1Setting = GameObject.FindObjectOfType<TownStage1Setting>();
                        if(townStage1Setting != null){
                            townStage1Setting.HideWall();
                        }
                        SetQuestStep(context, 10);
                    }
                    break;
            }
        }
    }
}