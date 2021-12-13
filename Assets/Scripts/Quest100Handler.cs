using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest100Handler:QuestHandler{
    public override void OnAction(string actionName, Dictionary<string, object> context) {
        if(actionName == "EndOfTalk"){
            string sceneName = (string)context["sceneName"];
            if(sceneName == "townstage"){
                SceneManager.LoadScene("townstage1");
            }
        }
    }
}