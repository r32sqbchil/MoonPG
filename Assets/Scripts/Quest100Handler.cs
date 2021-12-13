using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest100Handler:QuestHandler{
    const string TOWNSTAGE = "townstage";
    const string TOWNSTAGE1 = "townstage1";

    public override void OnAction(string actionName, Dictionary<string, object> context) {
        if(actionName == QuestHandler.EVENT_END_OF_TALK){
            string sceneName = (string)context[QuestHandler.KEY_OF_SCENE_NAME];
            if(sceneName == TOWNSTAGE){
                SceneManager.LoadScene(TOWNSTAGE1);
            } else if(sceneName == TOWNSTAGE1){
                if(GetQuestStep(context) == 0) {
                    GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
                    gameManager.SetLimitMoveXMax("MovingAreaX3");
                    SetQuestStep(context, 10);
                }
            }
        }
    }
}