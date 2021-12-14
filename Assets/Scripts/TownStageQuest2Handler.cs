using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStageQuest2Handler:QuestHandler{
    const string KEY_OF_HUNTING_COUNT = "huntingCount";
    const string KEY_OF_MISSION_COUNT = "missionCount";

    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        string sceneName = (string)context["sceneName"];

        //if(sceneName == "townstage")
        if(actionName == EVENT_END_OF_TALK){
            int step = GetQuestStep(context);
            if(step == 0) {
                context[KEY_OF_HUNTING_COUNT] = 0;
                context[KEY_OF_MISSION_COUNT] = 3;

                GameObject portal = GetPortalObject();
                if(portal != null) {
                    portal.SetActive(true);
                } else {
                    Debug.LogWarning("not found portal object");
                }

                SetQuestStep(context, 10);

                QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                questManager.AddUpdateHandler(this, context);
            } else if(step == 10) {
                // 목표 도달 여부를 확인해야 함!!
            } else if(step == 20) {
                // 퀘스트 성공했을 때 처리할 내용
            }
        } else if(actionName == EVENT_UPDATE) {
            // 프레임 호출을 전달 받음
        } else if(actionName == EVENT_NOTIFY) {
            GameObject actionObject = (GameObject)context[QuestHandler.KEY_OF_ACTION_OBJECT];
            string notifyName = (string)context[QuestHandler.KEY_OF_NOTIFY_NAME];

            if(notifyName == GameManager.ACTION_ON_DIED){
                int huntingCount = (int)context[KEY_OF_HUNTING_COUNT];
                int missionCount = (int)context[KEY_OF_MISSION_COUNT];
                huntingCount++;
                if(missionCount <= huntingCount) {
                    QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                    questManager.RemoveUpdateHandler(this);
                }
            }
        }
    }

    GameObject GetPortalObject(){
        foreach(GameObject go in GameObject.FindObjectsOfType<GameObject>(true)){
            if(go.name == "Portal"){
                return go;
            }
        }
        return null;
    }
}