using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownStage1Setting : MonoBehaviour
{
    private GameManager gameManager;
    private QuestHandler talk4Trigger;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetLimitMoveXMax("MovingAreaX2");

        QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
        string sceneName = SceneManager.GetActiveScene().name;
        Dictionary<string, object> context = questManager.GetQuestContext(sceneName, 0, 0);
        questManager.AddUpdateHandler(talk4Trigger = new Talk4Trigger(gameManager), context);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HideWall()
    {
        if(talk4Trigger!=null)
        {
            QuestManager questManager = GameObject.FindObjectOfType<QuestManager>();
            questManager.RemoveUpdateHandler(talk4Trigger);
            gameManager.SetLimitMoveXMax("MovingAreaX9");
        }
    }

    class Talk4Trigger : QuestHandler
    {
        private GameManager gameManager;
        private GameObject playerObject;

        public Talk4Trigger(GameManager gameManager)
        {
            this.gameManager = gameManager;
            Player player = GameObject.FindObjectOfType<Player>();
            if(player != null){
                playerObject = player.gameObject;
            }
        }

        public override void OnAction(string actionName, Dictionary<string, object> context)
        {
            if(actionName == EVENT_NOTIFY){
                string notifyName = (string)context[KEY_OF_NOTIFY_NAME];
                if(notifyName == GameManager.ACTION_ON_TOUCH_RIGHT_BOUNDARY){
                    gameManager.Action(playerObject);
                }
            }
        }
    }
}
