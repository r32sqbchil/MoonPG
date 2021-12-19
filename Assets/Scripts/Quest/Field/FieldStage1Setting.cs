using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldStage1Setting : MonoBehaviour
{
    const string KEY_OF_HUNTING_COUNT = "huntingCount";
    const string KEY_OF_MISSION_COUNT = "missionCount";

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetLimitMoveXMax("MovingAreaX2");

        QuestManager questManager = GameManager.FindQuestManager();
        string sceneName = SceneManager.GetActiveScene().name;
        Dictionary<string, object> context = questManager.GetQuestContext(sceneName, 500, 0);
        context[KEY_OF_HUNTING_COUNT] = 0;
        context[KEY_OF_MISSION_COUNT] = 25;
        questManager.AddUpdateHandler(new HuntingMonsterHandler(this), context);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HideWall()
    {
        gameManager.SetLimitMoveXMax("MovingAreaX9");
    }

    class HuntingMonsterHandler : QuestHandler
    {
        private FieldStage1Setting setting;

        public HuntingMonsterHandler(FieldStage1Setting setting){
            this.setting = setting;
        }
        public override void OnAction(string actionName, Dictionary<string, object> context)
        {
            if(actionName == EVENT_NOTIFY){
                GameObject actionObject = (GameObject)context[QuestHandler.KEY_OF_ACTION_OBJECT];
                string notifyName = (string)context[KEY_OF_NOTIFY_NAME];

                if(notifyName == GameManager.ACTION_ON_DIED){
                    int huntingCount = (int)context[KEY_OF_HUNTING_COUNT];
                    int missionCount = (int)context[KEY_OF_MISSION_COUNT];
                    SetContextValue(context, KEY_OF_HUNTING_COUNT, ++huntingCount);
                    if(missionCount <= huntingCount) {
                        Debug.Log("몬스터 사냥 미션 완료..벽 해제");
                        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                        questManager.RemoveUpdateHandler(this);
                        setting.Invoke("HideWall", 0.5f);
                    }
                }
            }
        }
    }
}
