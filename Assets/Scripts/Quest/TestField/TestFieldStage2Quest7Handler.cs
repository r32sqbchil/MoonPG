using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFieldStage2Quest7Handler : QuestHandler
{
    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        if(actionName == QuestHandler.EVENT_END_OF_TALK)
        {
            if(GetQuestStep(context) == 0)
            {
                TestFieldStage2Setting testFieldStage2Setting = GameObject.FindObjectOfType<TestFieldStage2Setting>();
                if(testFieldStage2Setting != null){
                    testFieldStage2Setting.HideWall();
                } else {
                    Debug.LogWarning("Can't find a component - TestFieldStage2Setting");
                }
                SetQuestStep(context, 10);
            }
        }
    }
}
