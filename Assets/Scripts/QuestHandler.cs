using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler
{
    public const string EVENT_END_OF_TALK = "EndOfTalk";
    public const string EVENT_UPDATE = "Update";
    public const string EVENT_NOTIFY = "Notify";
    public const string KEY_OF_STEP = "step";
    public const string KEY_OF_EVENT_DATA = "eventData";


    public virtual void OnAction(string actionName, Dictionary<string, object> context) {
        //no action
    }

    public virtual int GetQuestStep(Dictionary<string, object> context){
        if(context.ContainsKey(KEY_OF_STEP)){
            return (int)context[KEY_OF_STEP];
        } else {
            return 0;
        }
    }

    public virtual void SetQuestStep(Dictionary<string, object> context, int step){
        context.Add(KEY_OF_STEP, step);
    }
}
