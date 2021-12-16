using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainStageQuest2Handler : QuestHandler
{
    const string KEY_OF_HUNTING_COUNT = "huntingCount";
    const string KEY_OF_MISSION_COUNT = "missionCount";

    public override void OnAction(string actionName, Dictionary<string, object> context)
    {
        string sceneName = (string)context["sceneName"];

        if(actionName == EVENT_END_OF_TALK){
            int step = GetQuestStep(context);
            if(step == 0) {
                SetContextValue(context, KEY_OF_HUNTING_COUNT, 0);
                SetContextValue(context, KEY_OF_MISSION_COUNT, 3);

                MountainStageSetting setting = GameObject.FindObjectOfType<MountainStageSetting>();
                if(setting) setting.ActivatePortal(GetPortalObject(), context);

                SetQuestStep(context, 10);

                QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                questManager.AddUpdateHandler(this, context);
            } else if(step == 10) {
                // 목표 도달 여부를 확인해야 함!!
                int huntingCount = (int)context[KEY_OF_HUNTING_COUNT];
                int missionCount = (int)context[KEY_OF_MISSION_COUNT];
                Debug.Log("Quest "+huntingCount+"/"+missionCount);
            } else if(step == 20) {
                // 퀘스트 성공했을 때 처리할 내용
                SetQuestStep(context, 30);
            }
        } else if(actionName == EVENT_UPDATE) {
            // 프레임 호출을 전달 받음
        } else if(actionName == EVENT_NOTIFY) {
            GameObject actionObject = (GameObject)context[QuestHandler.KEY_OF_ACTION_OBJECT];
            string notifyName = (string)context[QuestHandler.KEY_OF_NOTIFY_NAME];

            if(notifyName == GameManager.ACTION_ON_DIED){
                int huntingCount = (int)context[KEY_OF_HUNTING_COUNT];
                int missionCount = (int)context[KEY_OF_MISSION_COUNT];
                SetContextValue(context, KEY_OF_HUNTING_COUNT, ++huntingCount);
                if(missionCount <= huntingCount) {
                    Debug.Log("몬스터 사냥 미션 완료..마을로 돌아가 보상을 받으셈");
                    QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                    questManager.RemoveUpdateHandler(this);
                    SetQuestStep(context, 20);
                }
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
