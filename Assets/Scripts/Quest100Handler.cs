using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest100Handler:QuestHandler{
    public override void OnAction(string actionName, object context) {
        Debug.Log("OnAction: " + context);
        if(actionName == "EndOfTalk"){
            SceneManager.LoadScene("townstage1");
        }
    }
}