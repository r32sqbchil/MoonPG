using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownStage2Setting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestManager questManager = GameManager.FindQuestManager();
        string sceneName = SceneManager.GetActiveScene().name;
        Dictionary<string, object> context = questManager.GetQuestContext(sceneName, 0, 0);
        questManager.AddUpdateHandler(new HuntingMonsterHandler(this), context);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextScene()
    {
        SceneManager.LoadScene(QuestHandler.SCENE_TOWN_STAGE3);
    }

    class HuntingMonsterHandler : QuestHandler
    {
        private TownStage2Setting setting;

        public HuntingMonsterHandler(TownStage2Setting setting){
            this.setting = setting;
        }
        public override void OnAction(string actionName, Dictionary<string, object> context)
        {
            if(actionName == EVENT_NOTIFY){
                string notifyName = (string)context[KEY_OF_NOTIFY_NAME];
                if(notifyName == GameManager.ACTION_ON_DIED){
                    QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
                    questManager.RemoveUpdateHandler(this);
                    setting.Invoke("NextScene", 2.5f);
                }
            }
        }
    }
}
