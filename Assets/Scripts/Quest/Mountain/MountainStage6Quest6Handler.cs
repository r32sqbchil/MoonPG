using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainStage6Quest6Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            int step = GetQuestStep(context);
            if(step == 0) {
                SetQuestStep(context, 10);
                SceneManager.LoadScene(SCENE_TESTFIELD_STAGE);
            }
        }
    }
}
