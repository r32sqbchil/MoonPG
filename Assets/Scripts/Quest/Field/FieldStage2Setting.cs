using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldStage2Setting : MonoBehaviour
{
    const string KEY_MONOLOGUE = "monologue";

    const string KEY_OF_HUNTING_COUNT = "huntingCount";
    const string KEY_OF_MISSION_COUNT = "missionCount";

    private GameManager gameManager;

    void Start()
    {
        QuestManager questManager = GameManager.FindQuestManager();
        SetupMonologue(questManager);

        string sceneName = SceneManager.GetActiveScene().name;
        Dictionary<string, object> context = questManager.GetQuestContext(sceneName, 500, 0);
        context[KEY_OF_HUNTING_COUNT] = 0;
        context[KEY_OF_MISSION_COUNT] = 3;
        questManager.AddUpdateHandler(new HuntingMonsterHandler(this), context);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupMonologue(QuestManager questManager)
    {
        Dictionary<string, object> context = questManager.GetQuestContext(Scene.SCENE_FIELD_STAGE2, 0, 0);
        if(!context.ContainsKey(KEY_MONOLOGUE))
        {
            context.Add(KEY_MONOLOGUE, true);
            Invoke("ShowMonologue", 1.0f);
        }
    }

    void ShowMonologue()
    {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        Player player = GameObject.FindObjectOfType<Player>();
        gameManager.Action(player.gameObject);
    }

    void NextScene()
    {
        SceneManager.LoadScene(QuestHandler.SCENE_CASTLE_STAGE);
    }

    class HuntingMonsterHandler : QuestHandler
    {
        private FieldStage2Setting setting;

        public HuntingMonsterHandler(FieldStage2Setting setting){
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
                        Debug.Log("몬스터 사냥 미션 완료.");
                        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                        questManager.RemoveUpdateHandler(this);
                        setting.Invoke("NextScene", 0.5f);
                    }
                }
            }
        }
    }
}
