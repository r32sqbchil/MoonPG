using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : Scene
{
    public const string EVENT_END_OF_TALK = "EndOfTalk";
    public const string EVENT_UPDATE = "Update";
    public const string EVENT_NOTIFY = "Notify";

    public const string KEY_OF_PERSISTENCE = "_persistence_";
    public const string KEY_OF_SCENE_NAME = "sceneName";
    public const string KEY_OF_OBJECT_ID = "objectId";
    public const string KEY_OF_TALK_INDEX = "talkIndex";
    
    public const string KEY_OF_STEP = "step";
    public const string KEY_OF_ACTION_OBJECT = "actionObject";
    public const string KEY_OF_NOTIFY_NAME = "notifyName";


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
        SetContextValue(context, KEY_OF_STEP, step);
    }

    public static void SetContextValue(Dictionary<string, object> context, string key, object value){
        context[key] = value;
        if(context.ContainsKey(KEY_OF_PERSISTENCE)){
            Dictionary<string, object> origin = (Dictionary<string, object>)context[KEY_OF_PERSISTENCE];
            origin[key] = value;
        }
    }
}
